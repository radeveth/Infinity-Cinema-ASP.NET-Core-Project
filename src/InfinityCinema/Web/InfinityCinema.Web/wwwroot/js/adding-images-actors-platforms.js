// Form Models Elements
let movieFormModelElement = document.getElementById('movie-form-model');
let imageActorPlatformFormModelElement = document.getElementById('image-actor-platform-form-model');

// Buttons Elements
let nextStepMovieButtonElement = document.getElementById('next-step-btn');
let createMovieButtonElement = document.getElementById('create-movie-btn');

nextStepMovieButtonElement.addEventListener('click', (e) => {
    e.preventDefault();

    movieFormModelElement.classList.add('d-none');
    imageActorPlatformFormModelElement.classList.remove('d-none');

    createMovieButtonElement.classList.remove('d-none');
});

createMovieButtonElement.addEventListener('click', (e) => {
    nextStepMovieButtonElement.classList.add('d-none');
    movieFormModelElement.classList.remove('d-none');

    imageActorPlatformFormModelElement.children[0].children[0].classList.remove('mt-3');
    imageActorPlatformFormModelElement.children[0].children[0].classList.add('mt-4');
});

// Images
let addImageButtonElement = document.getElementById('add-image-btn');

var imagesIndex = 0;
addImageButtonElement.addEventListener('click', (e) => {
    e.preventDefault();

    $('#images-form-model').append("<p class='font-weight-bold mt-3' style='margin-bottom:3px'>Url: </p> <input name='Images[" + imagesIndex + "].ImageUrl' class='form-control' placeholder=''>");
    $('#images-form-model').append("<span asp-validation-for='Images.ImageUrl' class='small text-danger'></span>");
    $('#images-form-model').append("<hr>")
    imagesIndex++;
});

// Actors
let addActorButtonElement = document.getElementById('add-actor-btn');

var actorsIndex = 0;
addActorButtonElement.addEventListener('click', (e) => {
    e.preventDefault();

    $('#actors-form-model').append("<p class='font-weight-bold mt-3' style='margin-bottom:3px'>Full Name: </p> <input name='Actors[" + actorsIndex + "].FullName' class='form-control' placeholder=''>");
    $('#actors-form-model').append("<p class='font-weight-bold mt-2' style='margin-bottom:3px'>Image Url: </p> <input name='Actors[" + actorsIndex + "].ImageUrl' class='form-control' placeholder=''>");
    $('#actors-form-model').append("<p class='font-weight-bold mt-2' style='margin-bottom:3px'>Information Link: </p> <input name='Actors[" + actorsIndex + "].InformationLink' class='form-control' placeholder=''>");
    $('#actors-form-model').append("<hr>")
    actorsIndex++;
});

// Platforms
let addPlatformButtonElement = document.getElementById('add-platform-btn');
console.log(addPlatformButtonElement);

var platformsIndex = 0;
addPlatformButtonElement.addEventListener('click', (e) => {
    e.preventDefault();

    $('#platforms-form-model').append("<p class='font-weight-bold mt-3' style='margin-bottom:3px'>Name: </p> <input name='Platforms[" + platformsIndex + "].Name' class='form-control' placeholder=''>");
    $('#platforms-form-model').append("<p class='font-weight-bold mt-2' style='margin-bottom:3px'>Site Path: </p> <input name='Platforms[" + platformsIndex + "].PathUrl' class='form-control' placeholder=''>");
    $('#platforms-form-model').append("<p class='font-weight-bold mt-2' style='margin-bottom:3px'>Icon Url: </p>  <input name='Platforms[" + platformsIndex + "].IconUrl' class='form-control' placeholder=''>");
    $('#platforms-form-model').append("<hr>")
    platformsIndex++;
});