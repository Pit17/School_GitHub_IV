const cells = document.querySelectorAll('.cell');
const resetButton = document.querySelector('#reset');
const board = document.querySelector('.board');
const msg = document.querySelector('#msg');

let currentPlayer = 'x';
let gameOver = false;

function handleCellClick(event) {
  const cell = event.target;
  if (cell.classList.contains('x') || cell.classList.contains('o') || gameOver) {
    return;
  }
  cell.classList.add(currentPlayer);
  cell.textContent = currentPlayer;
  checkWin();
  currentPlayer = currentPlayer === 'x' ? 'o' : 'x';
}

function checkWin() {
  const winningCombinations = [
    [1, 2, 3],
    [4, 5, 6],
    [7, 8, 9],
    [1, 4, 7],
    [2, 5, 8],
    [3, 6, 9],
    [1, 5, 9],
    [3, 5, 7]
  ];
  for (let i = 0; i < winningCombinations.length; i++) {
    const [a, b, c] = winningCombinations[i];
    if (hasClass(cells[a-1], currentPlayer) && hasClass(cells[b-1], currentPlayer) && hasClass(cells[c-1], currentPlayer)) {
      gameOver = true;
	  msg.textContent = "Vince " + currentPlayer;
      board.classList.add('game-over');
      return;
    }
  }
  if (isBoardFull()) {
    gameOver = true;
	msg.textContent = "Patta";
    board.classList.add('game-over');
    return;
  }
}

function isBoardFull() {
  for (let i = 0; i < cells.length; i++) {
    if (!cells[i].classList.contains('x') && !cells[i].classList.contains('o')) {
      return false;
    }
  }
  return true;
}

function handleResetButtonClick() {
  for (let i = 0; i < cells.length; i++) {
    cells[i].classList.remove('x', 'o');
    cells[i].textContent = '';
  }
  board.classList.remove('game-over');
  gameOver = false;
  msg.textContent = "";
  currentPlayer = 'x';
}

async function InviaMossaAlServer(id_partita, row, col) {
  const res = await fetch(`/muovi/${id_partita}?row=${row}&col=${col}`);

  alert(res.body);  // TODO: la risposta va esaminata in base al protocollo
}

resetButton.addEventListener('click', handleResetButtonClick);  // NOTA: qui handleResetButtonClick non viene chiamato ma indicato come *delegato*

for (let i = 0; i < cells.length; i++) {
  cells[i].addEventListener('click', handleCellClick);
}

function hasClass(el, className) {
  return el.classList.contains(className);
};
