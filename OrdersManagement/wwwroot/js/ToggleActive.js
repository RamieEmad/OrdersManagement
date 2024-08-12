const statusLink = document.getElementById('statusLink');

statusLink.addEventListener('click', () => {
    statusLink.classList.toggle('active');
    statusLink.classList.toggle('deactive');
    statusLink.textContent = statusLink.classList.contains('active') ? 'Active' : 'DeActive';
});