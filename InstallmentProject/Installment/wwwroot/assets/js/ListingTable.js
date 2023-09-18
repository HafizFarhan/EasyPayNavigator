$(document).ready(function () {
    // Hide the Actions button by default
    $('#bulkActionDropdown').hide();

    var selectOnAllPages = document.getElementById('selectAllRecords').value;
    if (selectOnAllPages == 'True') {
        var checkbox = document.getElementById('select-all');
        checkbox.click();
        $('#bulkActionDropdown').show();
        document.getElementById('allselectedAlert').style.display = 'block';
    }

    // Handle checkbox change event
    $('.row-check').change(function () {
        // Check if any checkbox is selected
        if ($('.row-check:checked').length > 0) {
            document.getElementById('select-all-container').classList.add('select-all-checked');
            // Show the Actions button
            $('#bulkActionDropdown').show();
        } else {
            document.getElementById('select-all-container').classList.remove('select-all-checked');
            // Hide the Actions button
            $('#bulkActionDropdown').hide();
        }
    });

    $('#select-all').change(function () {
        // Check if select-all checkbox is selected
        updateSelectedCount();
        if ($('#select-all:checked').length > 0) {
            document.getElementById('select-all-container').classList.add('select-all-checked');
            // Show the Actions button
            $('#bulkActionDropdown').show();
        } else {
            document.getElementById('select-all-container').classList.remove('select-all-checked');
            // Hide the Actions button
            $('#bulkActionDropdown').hide();
        }
    });
    document.getElementById('selectOnThisPage').addEventListener('click', function (event) {
        var selectOnAllPages = document.getElementById('selectAllRecords');
        selectOnAllPages.value = "False"
        var checkbox = document.getElementById('select-all');
        if (!checkbox.checked)
            checkbox.click();
        updateSelectedCount();
        document.getElementById('allselectedAlert').style.display = 'none';
        document.getElementById('selectedAlert').style.display = 'block';

        var links = document.querySelectorAll('.pages-link');
        links.forEach(function (link) {
            var href = link.getAttribute('href');
            if (href.includes('selectAllRecords=')) {
                href = href.replace(/selectAllRecords=([^&]+)/, 'selectAllRecords=false');
            } else {
                if (href.includes('?')) {
                    href += '&selectAllRecords=false';
                } else {
                    href += '?selectAllRecords=false';
                }
            }
            link.setAttribute('href', href);
        });
    });
    document.getElementById('selectAllLink').addEventListener('click', function (event) {
        var selectOnAllPages = document.getElementById('selectAllRecords');
        selectOnAllPages.value = "True";
        var checkbox = document.getElementById('select-all');
        if (!checkbox.checked)
            checkbox.click();
        document.getElementById('selectedAlert').style.display = 'none';
        document.getElementById('allselectedAlert').style.display = 'block';

        var links = document.querySelectorAll('.pages-link');
        links.forEach(function (link) {
            var href = link.getAttribute('href');
            if (href.includes('selectAllRecords=')) {
                href = href.replace(/selectAllRecords=([^&]+)/, 'selectAllRecords=true');
            } else {
                if (href.includes('?')) {
                    href += '&selectAllRecords=true';
                } else {
                    href += '?selectAllRecords=true';
                }
            }
            link.setAttribute('href', href);
        });
    });
});

const checkboxes = document.querySelectorAll('.row-check');
var isFormSubmitted = false;
// Add event listener to each checkbox
checkboxes.forEach(checkbox => {
    checkbox.addEventListener('change', updateSelectedCount);

});

function updateSelectedCount() {
    // Get all selected checkboxes
    const selectedCheckboxes = document.querySelectorAll('.row-check:checked');


    // Update the text in the #records element
    const selectedRecordsElement = document.getElementById('selectedRecords');
    selectedRecordsElement.textContent = selectedCheckboxes.length;

    // Show or hide the alert based on the number of selected checkboxes
    if (selectedCheckboxes.length > 0) {
        document.getElementById('selectedAlert').style.display = 'block';
        document.getElementById('allselectedAlert').style.display = 'none';
    } else {
        document.getElementById('selectedAlert').style.display = 'none';
    }

}



const selectAllCheckbox = document.querySelector('#select-all');
const rowCheckboxes = document.querySelectorAll('.row-check');
const bulkActionButtons = document.querySelectorAll('.bulk-action-button');


// Function to get all selected product IDs
function getSelectedProductIds() {
    return Array.from(rowCheckboxes)
        .filter(checkbox => checkbox.checked)
        .map(checkbox => checkbox.dataset.productid);
}

// Add event listener to select all checkbox
selectAllCheckbox.addEventListener('change', () => {
    for (const checkbox of rowCheckboxes) {
        checkbox.checked = selectAllCheckbox.checked;
        toggleRowColor(checkbox);
    }
});

// Add event listener to row checkboxes
for (const checkbox of rowCheckboxes) {
    checkbox.addEventListener('change', () => {
        const allChecked = Array.from(rowCheckboxes).every(cb => cb.checked);
        const someChecked = Array.from(rowCheckboxes).some(cb => cb.checked);
        selectAllCheckbox.checked = allChecked;
        selectAllCheckbox.indeterminate = !allChecked && someChecked;
    });
}

// Make the cells in the row clickable in a way that it will check the checkbox in the row and toggle the light-bg class
document.querySelectorAll('.clickable').forEach(function (ele) {
    ele.addEventListener('click', function (e) {
        if (!$(e.target).hasClass('row-check')) {
            const checkbox = this.closest('tr').querySelector('.row-check');
            checkbox.checked = !checkbox.checked;
            checkbox.dispatchEvent(new Event('change'));
        }
    });
});


document.querySelectorAll('.row-check').forEach(function (ele) {
    ele.addEventListener('change', function () {
        toggleRowColor(this);
    });
});

function toggleRowColor(checkbox) {
    if (checkbox.checked) {
        checkbox.closest('tr').classList.add('light-bg');
    } else {
        checkbox.closest('tr').classList.remove('light-bg');
    }
}

document.getElementById('pageNumberInput').addEventListener('keyup', function (event) {
    if (event.keyCode === 13) {

        var pageSize = document.querySelector('select[name="pageSize"]').value;
        var pageNumber = document.getElementById('pageNumberInput').value;
        var selectAllRecords = document.getElementById('selectAllRecords').value;
        var url = '?pageNumber=' + pageNumber + '&pageSize=' + pageSize + '&selectAllRecords=' + selectAllRecords;
        window.location.href = url;
    }
});

function togglePopup() {
    var popupContainer = document.getElementById("popupContainer");
    popupContainer.style.display = "flex";
    popupContainer.addEventListener("click", closeOnOutsideClick);
    setTimeout(function () {
        popupContainer.querySelector(".popup").style.left = "0";
    }, 100);
}

function closePopup() {
    var popupContainer = document.getElementById("popupContainer");
    popupContainer.style.display = "none";
    popupContainer.removeEventListener("click", closeOnOutsideClick);
    popupContainer.querySelector(".popup").style.left = "-100%";
}

function closeOnOutsideClick(event) {
    if (event.target === event.currentTarget) {
        closePopup();
    }
}

function setRecordsPerPage(pageSize) {
    var selectAllRecords = document.getElementById('selectAllRecords').value;
    var url = '?pageNumber=1' + '&pageSize=' + pageSize + '&selectAllRecords=' + selectAllRecords;
    window.location.href = url;
}

/*Filters*/
function applyFilters() {
    document.getElementById("filterform").submit();
}

/*Search*/
function submitSearch() {
    document.getElementById("search-form").submit();
}

/*Loading Modal*/
function showLoadingModal() {
    // Close the confirmation modal
    var confirmationModal = bootstrap.Modal.getInstance(document.getElementById("confirmationModal"));
    confirmationModal.hide();

    // Show the loading modal
    var loadingModal = new bootstrap.Modal(document.getElementById("loadingModal"));
    loadingModal.show();
}

function formatDate(inputDate) {
    if (!(inputDate instanceof Date)) {
        throw new Error('Invalid date input');
    }

    const options = { year: 'numeric', month: 'long', day: 'numeric' };
    const formattedDate = inputDate.toLocaleDateString(undefined, options);
    return formattedDate;
}

//Show confirmation model
function showConfirmationModal(button, btnType) {
    var buttonId = button.getAttribute("id");
    if (btnType == "bulk") {
        var selectedIds = Array.from(document.querySelectorAll(".row-check:checked")).map(el => el.dataset.productid);
        var selectOnAllPages = document.getElementById('selectAllRecords').value;
        if (selectOnAllPages == "True")
            document.getElementById("dataIdField").value = 'all'; 
        else
            document.getElementById("dataIdField").value = selectedIds.join(',');
    }
    else {
        var dataSourceId = button.getAttribute("data-source-id");
        var dataSourceIdField = document.getElementById("dataIdField");
        dataSourceIdField.value = dataSourceId;
    }
    // Set the title, message, and loading title based on the action
    const { title, message, loadingTitle, handler } = setModalText(buttonId);

    // Set the title and message of the confirmation modal
    document.getElementById("confirmationModalLabel").innerText = title;
    document.getElementById("confirmationModalMessage").innerText = message;

    // Set the title of the loading modal
    document.getElementById("loadingModalLabel").innerText = loadingTitle;

    // Set the formaction attribute for the submit button
    document.getElementById("continueBtn").setAttribute("formaction", handler);

    // Show the confirmation modal
    var confirmationModal = new bootstrap.Modal(document.getElementById("confirmationModal"));
    confirmationModal.show();
}
function isValidPhoneNumber(phoneNumber) {
    var phoneRegex = /^(\+\d{1,2}\s?)?\(?\d{3}\)?[\s.-]?\d{3}[\s.-]?\d{4}$/;
    return phoneRegex.test(phoneNumber);
}
function isValidEmail(email) {
    var emailRegex = /^[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,}$/;
    return emailRegex.test(email);
}

//Toggle Favourite
function toggleFavorite(isFavorite) {
    if (isFavorite) {
        window.location = window.location.href.split("?")[0];
    } else {
        window.location.href = '?isfavorite=true';
    }
}