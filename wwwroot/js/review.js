document.addEventListener('DOMContentLoaded', function () {
    const stars = document.querySelectorAll('#rating i');
    const ratingValueInput = document.getElementById('ratingValue');

    stars.forEach(star => {
        star.addEventListener('mouseover', handleMouseOver);
        star.addEventListener('mouseout', handleMouseOut);
        star.addEventListener('click', handleClick);
    });

    function handleMouseOver(event) {
        const value = event.target.dataset.value;
        stars.forEach(star => {
            if (star.dataset.value <= value) {
                star.classList.add('text-warning');
            } else {
                star.classList.remove('text-warning');
            }
        });
    }

    function handleMouseOut() {
        const value = ratingValueInput.value;
        stars.forEach(star => {
            if (star.dataset.value <= value) {
                star.classList.add('text-warning');
            } else {
                star.classList.remove('text-warning');
            }
        });
        // Remove hover effect
        stars.forEach(star => star.classList.remove('text-warning-hover'));
    }

    function handleClick(event) {
        const value = event.target.dataset.value;
        ratingValueInput.value = value;
        stars.forEach(star => {
            if (star.dataset.value <= value) {
                star.classList.add('text-warning');
            } else {
                star.classList.remove('text-warning');
            }
        });
    }
});
