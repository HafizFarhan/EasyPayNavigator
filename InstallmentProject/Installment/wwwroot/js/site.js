
// Function to handle the click event on the dashboard link

// Write your JavaScript code.
//original one
//function makeMeActive(elementId, status) {
//    /*console.log("makeMeActive function called with elementId:", elementId);*/
//    $('.nav-link').removeClass('active');
//    $('.nav-item').removeClass('active');
//    $('#' + elementId + '').addClass('active');

//}

document.addEventListener("DOMContentLoaded", function () {
    feather.replace(); // Initialize Feather Icons

    const togglePasswordIcons = document.querySelectorAll('.toggle-password');
    togglePasswordIcons.forEach((icon) => {
        icon.addEventListener('click', function () {
            const passwordInput = icon.closest('.input-group').querySelector('.password-input-padding');
            passwordInput.type = passwordInput.type === 'password' ? 'text' : 'password';

            // Toggle Feather Icons
            const currentIcon = icon.getAttribute('data-feather');
            const newIcon = currentIcon === 'eye' ? 'eye-off' : 'eye';
            icon.setAttribute('data-feather', newIcon);
            icon.innerHTML = feather.icons[newIcon].toSvg(); // Update the icon content
        });
    });
});


jQuery(document).ready(function ($) {
   

    if ($(".js-example-basic-single").length) {
        $(".js-example-basic-single").select2();
    }
    if ($(".js-example-basic-multiple").length) {
        $(".js-example-basic-multiple").select2();
    }
    var CustomSelectionAdapter = $.fn.select2.amd.require("select2/selection/customSelectionAdapter");

    $(".js-example-basic-multiple").select2({
        // options 
        selectionAdapter: CustomSelectionAdapter
    });

    $('.select2-arrow').append('<i class="link-arrow" data-feather="chevron-down"></i>');
    //var CustomSelectionAdapter = $.fn.select2.amd.require("select2/selection/customSelectionAdapter");

    //$("select").select2({
    //    // options
    //    selectionAdapter: CustomSelectionAdapter,
    //    selectionContainer: $('.js-example-basic-multiple')
    //});
});

// Function to handle the click event on the dashboard link
function handleDashboardClick() {
    // Remove the 'active' class from all nav-links and nav-items
    console.log("handleDashboardClick called");
    document.querySelectorAll('.nav-link').forEach(link => link.classList.remove('active'));
    document.querySelectorAll('.nav-item').forEach(item => item.classList.remove('active'));


    ////// Add the 'active' class to the dashboard link and nav-item
    document.getElementById('dashboardNav').classList.add('active');

    //// Hide the products submenu
    document.getElementById('products').classList.remove('show');
    // Add the 'active' class to the clicked link and its parent nav-item
    
}


function makeMeActive(elementId) {
    const currentURL = window.location.pathname;
    $('.nav-link').removeClass('active');
    $('.nav-item').removeClass('active');
    $('#' + elementId + '').addClass('active');
    var nav = $('#' + elementId + '').closest('.main-item')
    nav[0].classList.add('active');
}

function navigateMe(link) {
    window.location.href = link;
    debugger;
}

function adjustPageContentMargin() {
    const alertContainer = document.querySelector('.alert-container');
    const pageContentElement = document.querySelector('.page-content');
    const navBarElement = document.querySelector('.navbar');
    const sideBarElement = document.querySelector('.sidebar');

    const hasActiveNotification = notificationData && notificationData.Content && alertContainer.childElementCount > 0;
    
    if (false)
    {
        // If there's an active notification and the alert container has content, adjust margins
        pageContentElement.classList.add('adjusted');
        navBarElement.classList.add('adjusted');
        sideBarElement.classList.add('adjusted');
    } else {
        // If there's no active notification or the alert container is empty, revert to original margins
        pageContentElement.classList.remove('adjusted');
        navBarElement.classList.remove('adjusted');
        sideBarElement.classList.remove('adjusted');
    }
}
// Declare the notification object with sample data (or fetch from API if available)
const notificationData = {
    Type: 0, // Set the type based on your enumeration (0 = Primary, 1 = Secondary, etc.)
    Title: "Confirmation Needed",
    Content: "Your email address still needs confirmation. Confirm your email to secure your account and unlock the full potential of our cutting-edge AI tools.",
    Action: "~/DesginSystem",
    ActionText: "Resend Email Confirmation"
};
document.addEventListener('DOMContentLoaded', function () {

    // Call the function initially to adjust the margin based on the initial state
    adjustPageContentMargin();
    // Check if 'notificationData' is defined and not null
    //if (typeof notificationData !== 'undefined' && notificationData !== null) {
    if (false) {

        // Use MutationObserver to observe changes in the DOM and adjust the margin accordingly
        const observer = new MutationObserver(adjustPageContentMargin);
        observer.observe(document.body, { childList: true, subtree: true });
        // Call the function to update the alert div immediately
        updateAlertDiv();
        // Call the function to update the alert div every 10 minutes (600,000 milliseconds)
        setInterval(() => updateAlertDiv(notificationData), 600000); // 10 minutes = 600,000 milliseconds
        //    updateAlertDiv();
    }
});
function updateAlertDiv() {
    // Get the parent container
    var alertContainer = document.getElementById('topNotifications');
    // If a notification exists, create the alert div
    if (notificationData != null) {
        // Create the alert div based on the notification data
        var alertDiv = createAlertDiv(notificationData);
        // Replace the existing content of alertContainer with the new alertDiv
        alertContainer.innerHTML = ''; // Clear existing content
        alertContainer.appendChild(alertDiv); // Add the new alertDiv
    }
}
// Function to create the alert div based on the `TopNotification` model data
function createAlertDiv(notification) {
    const alertClasses = {
        0: 'alert alert-primary',   // eTopNotification.Primary
        1: 'alert alert-secondary', // eTopNotification.Secondary
        2: 'alert alert-success',   // eTopNotification.Success
        3: 'alert alert-danger',    // eTopNotification.Danger
        4: 'alert alert-warning',   // eTopNotification.Warning
    };

    const alertDiv = document.createElement('div');
    alertDiv.className = alertClasses[notification.Type] || 'alert'; // Default alert class

    alertDiv.setAttribute('role', 'alert');

    // The following line adds the style to center align the text within the alert div
    alertDiv.style.textAlign = 'center';

    // Title (wrapped in <b> tags)
    const titleElement = document.createElement('b');
    titleElement.textContent = notification.Title;
    alertDiv.appendChild(titleElement);

    // Content
    alertDiv.appendChild(document.createTextNode(' ' + notification.Content));

    // Action (wrapped in <a> tags)
    const actionElement = document.createElement('a');
    actionElement.setAttribute('href', notification.Action);
    actionElement.textContent = notification.ActionText;

    alertDiv.appendChild(actionElement);

    return alertDiv;
}

document.addEventListener('DOMContentLoaded', function () {
    const productsNav = document.getElementById('productsNav');
    const productsSubMenu = document.getElementById('products');
    const productsListNav = document.getElementById('productsListNav');
    const parentNavItem = productsListNav.closest('.nav-item'); // Get the parent <li> element

    let productsSubMenuOpen = false;
    //parentNavItem.classList.add('active');
    function toggleProductsSubMenu() {
        //console.log("toggleProductsSubMenu called");
        productsSubMenuOpen = !productsSubMenuOpen;
        productsSubMenu.classList.toggle('show', productsSubMenuOpen);
        // Add the 'active' class to the parent nav-item when the sub-menu is open
        parentNavItem.classList.toggle('active', productsSubMenuOpen);
       
        //console.log("parent nav : "+parentNavItem);
    }

    function closeProductsSubMenu() {
        productsSubMenuOpen = false;
        productsSubMenu.classList.remove('show');

        // Remove the 'active' class from the parent nav-item when the sub-menu is closed
        //parentNavItem.classList.remove('active');
    }

    function handleDocumentClick(event) {
        const target = event.target;
        if (!target.closest('#productsNav') && !target.closest('#products')) {
            closeProductsSubMenu();
        }
    }

    productsNav.addEventListener('click', (event) => {
        event.preventDefault();
        toggleProductsSubMenu();
    });

    productsSubMenu.addEventListener('click', (event) => {
        event.stopPropagation();
    });

    document.addEventListener('click', handleDocumentClick);

    // Check if the current page URL matches the products sub-menu link href
    if (productsListNav && window.location.pathname === productsListNav.getAttribute('href')) {
        productsSubMenuOpen = true;
        productsSubMenu.classList.add('show');
        parentNavItem.classList.add('active');
    }
});

//Dropdown button designsystem
document.addEventListener("DOMContentLoaded", function () {
    const dropdownMenu = document.getElementById('customDropdownMenu');
    const mainButton = document.querySelector('.dropdown-button');
    const arrowButton = document.querySelector('.dropdown-toggle-split');

    mainButton.addEventListener('click', function () {
        dropdownMenu.style.left = '0';
    });
    
    arrowButton.addEventListener('click', function (event) {
        const mainButtonRect = mainButton.getBoundingClientRect();
        dropdownMenu.style.left = `${-mainButtonRect.left +125}px`;
        
    });
});
/*action button onclick handle*/

// JavaScript to apply the outline and shadow on click
$(document).ready(function () {
    var $actionButton = $("#actionButton");

    $actionButton.on("click", function (event) {
        event.stopPropagation(); // Prevent the click event from bubbling up to the document
        $actionButton.addClass("clicked"); // Add the 'clicked' class to apply the styles
    });

    // Handle click event on the document
    $(document).on("click", function (event) {
        if (!$(event.target).closest(".btn-group").length) {
            // Click occurred outside the button and its dropdown menu
            $actionButton.removeClass("clicked"); // Remove the 'clicked' class to reset the button's appearance
        }
    });
});
/*favorite icon fill handled*/
$(document).ready(function () {
    // Click event handler for the icon
    $(".fav-prod-a").click(function () {
        // Toggle the "filled" class on the anchor element to change its color on click
        $(this).toggleClass("filled");
    });
});

// tuesday chnages

document.addEventListener('DOMContentLoaded', function () {
    const productsNav = document.getElementById('productsNav');
    const productsSubMenu = document.getElementById('products');
    const productsListNav = document.getElementById('productsListNav');
    const parentNavItem = productsListNav.closest('.nav-item'); // Get the parent <li> element

    let productsSubMenuOpen = false;

    function toggleProductsSubMenu() {
        console.log("toggleProductsSubMenu called");
        productsSubMenuOpen = !productsSubMenuOpen;
        productsSubMenu.classList.toggle('show', productsSubMenuOpen);
        // Add the 'active' class to the parent nav-item when the sub-menu is open
        parentNavItem.classList.toggle('active', productsSubMenuOpen);
    }

    function closeProductsSubMenu() {
        productsSubMenuOpen = false;
        productsSubMenu.classList.remove('show');

        // Remove the 'active' class from the parent nav-item when the sub-menu is closed
        parentNavItem.classList.remove('active');
    }

    function handleDocumentClick(event) {
        const target = event.target;
        if (!target.closest('#productsNav') && !target.closest('#products')) {
            closeProductsSubMenu();
        }
    }

    productsNav.addEventListener('click', (event) => {
        event.preventDefault();
        toggleProductsSubMenu();
    });

    productsSubMenu.addEventListener('click', (event) => {
        event.stopPropagation();
    });

    document.addEventListener('click', handleDocumentClick);

    // Check if the current page URL matches the products sub-menu link href
    if (productsListNav && window.location.pathname === productsListNav.getAttribute('href')) {
        productsSubMenuOpen = true;
        productsSubMenu.classList.add('show');
        parentNavItem.classList.add('active');
    }
});
/*For dynamically generated select2 input field*/
   
 $('.js-example-basic-single ').one('select2:open', function (e) {
        $('input.select2-search__field').prop('placeholder', 'Search');
   });
    






   














