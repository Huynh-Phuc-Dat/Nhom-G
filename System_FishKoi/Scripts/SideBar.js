const SideBar = function ($http, $scope, $async) {

    const menuItems = document.querySelectorAll(".menu-item a");

    menuItems.forEach(item => {
        const arrayPath = window.location.pathname.split('/');
        if (item.getAttribute("href") === `/` + arrayPath[1]) {
            item.classList.add("active");
        }
    });
}
SideBar.$inject = ["$http", "$scope", "$async"];