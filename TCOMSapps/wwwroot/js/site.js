

//################ DRAG and DROP 
const fill = document.querySelector('.fill');
let current = document.querySelector('.current');
const empties = document.querySelectorAll('.empty');


// Fill Listeners
//fill.addEventListener('dragstart', dragStart);
//fill.addEventListener('dragend', dragEnd);

// Fill Listeners
current.addEventListener('dragstart', dragStart);
current.addEventListener('dragend', dragEnd);


// Loop through empties and call drag events
for (const empty of empties) {
    empty.addEventListener('dragover', dragOver);
    empty.addEventListener('dragenter', dragEnter);
    empty.addEventListener('dragleave', dragLeave);
    empty.addEventListener('drop', dragDrop);
}


// Drag Functions
function dragStart() {
    this.className += ' hold';
    this.className += ' current';
    setTimeout(() => (this.className = 'invisible'),0);
}

function dragEnd() {
    this.className += 'current';
}

function dragOver(e) {
    e.preventDefault();
}

function dragEnter(e) {
    e.preventDefault();
    this.className += ' hovered';
}

function dragLeave() {
    this.className += 'empty';
}

function dragDrop() {
    this.className = 'empty';
    console.log(this);
    this.append(current);
}

//####################