let saldo = 100;
let puntata = 0;

function TiraIDadi() {
    var dado1 = Math.floor(Math.random() * 6) + 1;
    var dado2 = Math.floor(Math.random() * 6) + 1;
   return dado1 + dado2;
}
function aggiungiSaldo(){
    if(saldo <= 0){
        saldo = 100;
        document.getElementById("saldo").innerHTML ="Il tuo saldo è :" + saldo + " sesterzi"; 
    }
    else if(saldo > 0){
        alert("Non puoi aggiungere soldi se ne hai già");
    }
}
function gioca(){
    puntata = document.getElementById("puntata").value;
    if(puntata > saldo ){
        alert("Non hai abbastanza soldi");
        return;
    }
    if(puntata <= 0){
        alert("Non puoi puntare soldi negativi");
        return;
    }

    saldo -= puntata;
    
    let numero = TiraIDadi();

    let scommessa = document.getElementById("scommessa").value;
    if(scommessa == numero){
        saldo += puntata * 2;
        document.getElementById("risultato").innerHTML = "Hai Vinto è uscito il numero " + numero;
    }
    else{
        document.getElementById("risultato").innerHTML = "Hai perso è uscito il numero " + numero;
    }
    document.getElementById("saldo").innerHTML ="Il tuo saldo è :" + saldo + " sesterzi"; 

}

