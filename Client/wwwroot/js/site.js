// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

window.onscroll = () => {
    const navbar = document.getElementById('navbar');
    const fixedNav = navbar.offsetTop;

    if (window.pageYOffset > fixedNav) {
        //  navbar.style.transform = 'translateY(0)';
        navbar.classList.add('bg-secondary', 'bg-opacity-75', 'backdrop-blur-md', 'shadow-md', 'shadow-slate-500', 'text-primary');
    } else {
        // navbar.style.transform = 'translateY(-100%)';
        navbar.classList.remove('bg-secondary', 'bg-opacity-75', 'backdrop-blur-md', 'shadow-md', 'shadow-slate-500', 'text-primary');
    }
};

const btnFaqs = document.querySelectorAll('#btn-faqs');
const faqsSectionContent = document.querySelectorAll('#faqs-section-content');

btnFaqs.forEach((btn, index) => {
    btn.addEventListener('click', function () {
        toggleAnimation(faqsSectionContent[index]);
    });
});

const toggleAnimation = (element) => {
    if (element.classList.contains('scale-y-0')) {
        element.classList.toggle('hidden');
        setTimeout(() => {
            element.classList.remove('scale-y-0');
            element.classList.add('scale-y-100');
        }, 0);
    } else {
        setTimeout(() => {
            element.classList.toggle('hidden');
        }, 300);
        element.classList.remove('scale-y-100');
        element.classList.add('scale-y-0');
    }
};