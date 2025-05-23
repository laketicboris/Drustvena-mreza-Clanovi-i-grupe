'use strict'

// Model klase
class Grupa {
    constructor(id, naziv) {
        this.id = id
        this.naziv = naziv
    }
}

// Inicijalizacija kada se stranica ucita
function initializeGrupe() {
    loadGrupe()

    // Dodaj Grupu gomb kezelése
    const dodajBtn = document.getElementById('dodajGrupuBtn')
    if (dodajBtn) {
        dodajBtn.addEventListener('click', () => {
            window.location.href = '../dodavanjeIzmenaGrupe/dodavanjeIzmenaGrupe.html'
        })
    }
}

// Ucitavanje svih grupa sa servera
function loadGrupe() {
    fetch('http://localhost:3891/api/grupa') 
        .then(response => {
            if (!response.ok) {
                throw new Error("Greška prilikom zahteva: " + response.status)
            }
            return response.json()
        })
        .then(grupe => {
            createGroupTable(grupe)
        })
        .catch(error => {
            console.error("Greška:", error.message)
            alert("Došlo je do greške prilikom učitavanja grupa.")
        })
}

// Kreiranje HTML tabele sa ucitanim grupama
function createGroupTable(grupe) {
    const container = document.querySelector("#grupaLista")
    container.innerHTML = `
        <table class="user-data">
            <thead class="user-data-head">
                <tr>
                    <th>ID</th>
                    <th>Naziv</th>
                    <th>Opcije</th>
                </tr>
            </thead>
            <tbody id="group-data-body"></tbody>
        </table>
    `
    const tbody = container.querySelector("#group-data-body")

    for (let grupa of grupe) {
        const row = document.createElement("tr")
        row.innerHTML = `
            <td>${grupa.id}</td>
            <td>${grupa.naziv}</td>
            <td>
              <button class="editGroupBtn" data-id="${grupa.id}">Izmeni</button>
              <button class="deleteGroupBtn" data-id="${grupa.id}">Obriši</button>
            </td>
        `

        // Izmeni gomb
        const editBtn = row.querySelector(".editGroupBtn")
        editBtn.addEventListener("click", () => {
            const id = editBtn.getAttribute('data-id')
            window.location.href = `../dodavanjeIzmenaGrupe/dodavanjeIzmenaGrupe.html?grupaId=${id}`
        })

        // Brisanje
        const deleteBtn = row.querySelector(".deleteGroupBtn")
        deleteBtn.addEventListener("click", () => {
            deleteGrupa(grupa.id)
        })

        tbody.appendChild(row)
    }
}

// Slanje delete zahteva
function deleteGrupa(id) {
    fetch(`http://localhost:3891/api/grupa/${id}`, {
        method: 'DELETE'
    })
    .then(res => {
        if (res.ok) {
            loadGrupe()
        } else {
            alert("Brisanje grupe nije uspelo.")
        }
    })
}

// Pokretanje prilikom ucitavanja stranice
document.addEventListener("DOMContentLoaded", initializeGrupe)
