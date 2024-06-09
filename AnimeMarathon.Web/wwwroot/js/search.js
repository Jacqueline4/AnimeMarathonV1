document.getElementById("searchInput").addEventListener("input", async function (event) {
    const searchTerm = event.target.value.trim();

    if (searchTerm !== "") {
        try {
            const response = await fetch(`https://localhost:7269/Anime/GetAnimeByName/${searchTerm}`);
            if (response.ok) {
                const data = await response.json();
                // Mostrar los resultados en el elemento #searchResultsContainer
                const searchResultsContainer = document.getElementById("searchResultsContainer");
                searchResultsContainer.innerHTML = ""; // Limpiar los resultados anteriores
                if (data.length === 0) {
                    searchResultsContainer.textContent = `No se encontraron animes con el nombre "${searchTerm}"`;
                } else {
                    data.forEach(anime => {
                        // Crear elementos para mostrar los resultados
                        const animeElement = document.createElement("div");
                        animeElement.classList.add("anime-item");
                       
                        const linkElement = document.createElement("a");
                        linkElement.href = `/AnimeDetail?id=${anime.id}`;

                        const imgElement = document.createElement("img");
                        imgElement.src = anime.posterImage;
                        imgElement.alt = `${anime.title} Poster`;
                        imgElement.style.maxWidth = "100%";

                        const titleElement = document.createElement("div");
                        titleElement.style.textAlign = "center";
                        titleElement.textContent = anime.title;
                        titleElement.style.fontSize = "16px"; 

                        linkElement.appendChild(imgElement);
                        linkElement.appendChild(titleElement);
                        animeElement.appendChild(linkElement);
                       
                        searchResultsContainer.appendChild(animeElement);
                    });

                }
            } else {
                console.error("Error al buscar animes:", response.statusText);
            }
        } catch (error) {
            console.error("Error en la solicitud fetch:", error);
        }
    } else {
        // Limpiar los resultados si el término de búsqueda está vacío
        const searchResultsContainer = document.getElementById("searchResultsContainer");
        searchResultsContainer.innerHTML = "";
    }
});
