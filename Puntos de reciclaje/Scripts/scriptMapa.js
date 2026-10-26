
// mapa es una variable y L.map es una funcion de leaflet para crear un objeto de tipo mapa
const mapa = L.map('mapa').setView([-34.534306, -58.792742], 13)

// Aca le damos un origen de datos a nuestro objeto de mapa, en este caso de openstreetmap.org
L.tileLayer('https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png').addTo(mapa)