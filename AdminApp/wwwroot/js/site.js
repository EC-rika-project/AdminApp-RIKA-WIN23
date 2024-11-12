
function FilterDivsOnSearch(letters) {

    const divs = document.querySelectorAll('.user-containers');

    divs.forEach(function (div) {

        if (!div.textContent.toLowerCase().includes(letters.toLowerCase())) {

            div.style.display = 'none';

        } else {

            div.style.display = 'flex';
        }

    });
}

function handleKeyUp(event) {

    const inputValue = event.target.value;

    FilterDivsOnSearch(inputValue);

}