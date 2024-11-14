document.addEventListener('DOMContentLoaded', function () {
    const navbar = document.querySelector('.navbar');
    const navItems = document.querySelectorAll('.nav-item');
    const menuToggle = document.querySelector('.menu-toggle');
    console.log('DOM fully loaded and parsed');
    if (navbar) {
        console.log("befor", navbar)
        navbar.style.display = 'none';
        console.log("after",navbar)
    } else {
        console.error('Navbar not found');
    }


    const navbarLite = document.querySelector('.navbar-lite');
    const toggleNavbarBtn = document.querySelector('.menu-toggle');

    toggleNavbarBtn.addEventListener('click', function () {
        navbarLite.classList.toggle('hidden');

    });


    const counterElement = document.getElementById('counter');
    const increaseBtn = document.getElementById('increaseBtn');
    const decreaseBtn = document.getElementById('decreaseBtn');
    const priceElement = document.getElementById('cart-price');
    const totalPriceElement = document.getElementById('cart-price-total');

    if (counterElement && increaseBtn && decreaseBtn && priceElement && totalPriceElement) {
        increaseBtn.addEventListener('click', function () {
            let currentValue = parseInt(counterElement.textContent);
            counterElement.textContent = currentValue + 1;
            updateTotalPrice();
        });

        decreaseBtn.addEventListener('click', function () {
            let currentValue = parseInt(counterElement.textContent);
            if (currentValue > 1) {
                counterElement.textContent = currentValue - 1;
                updateTotalPrice();
            }
        });
        function updateTotalPrice() {
            const price = parseFloat(priceElement.textContent.replace('vnd', '').replace(/\./g, '').trim());
            const quantity = parseInt(counterElement.textContent);
            const totalPrice = price * quantity;
            totalPriceElement.textContent = totalPrice.toLocaleString() + 'vnd';
        }
        updateTotalPrice();
    }
});


function toggleNavbar() {
    const navbar = document.querySelector('.navbar');
    const isNavbarVisible = navbar.style.display === 'flex';
    navbar.style.display = isNavbarVisible ? 'none' : 'flex';

    const navbarLite = document.querySelector('.navbar-lite');
    if (navbarLite) {
        navbarLite.classList.toggle('hidden');
    }
}

window.clearToken = function () {
    localStorage.removeItem('token');
};

function showSlides() {
    let slides = document.getElementsByClassName("slide");

    for (let i = 0; i < slides.length; i++) {
        slides[i].style.display = "none";
    }

    slideIndex++;
    if (slideIndex > slides.length) {
        slideIndex = 1;
    }

    slides[slideIndex - 1].style.display = "block";
    setTimeout(showSlides, 6000);
}

let slideIndex = 0;


showSlides();
