document.getElementById("searchResultsForm").addEventListener("submit", async function (event) {
    event.preventDefault();

    const formData = new FormData(event.target);
    const searchTitle = formData.get("searchTitle");
    const yearFrom = formData.get("yearFrom");
    const yearTo = formData.get("yearTo");
    const genre = formData.get("genre");
    const category = formData.get("category");
    const subtype = formData.get("subtype");
    const pegi = formData.get("pegi");

    // Construir el objeto de filtros
    const filters = {};
    if (searchTitle) {
        filters["nombre"] = searchTitle;
    }
    if (yearFrom) {
        filters.desde = parseInt(yearFrom);
    }
    if (yearTo) {
        filters.hasta = parseInt(yearTo);
    }
    if (genre) {
        filters.generoId = parseInt(genre); 
    }
    if (category) {
        filters.categoriaId = parseInt(category); 
    }
    if (pegi) {
        filters["pegi"] = pegi;
    }
    if (subtype) {
        filters["subtipo"] = subtype;
    }

    console.log("Filtros enviados:", filters);

    try {
        const response = await fetch("https://localhost:7269/Anime/GetAnimes", {
            method: "POST",
            headers: {
                "Content-Type": "application/json"
            },
            body: JSON.stringify(filters)
        });

        if (response.ok) {
            const data = await response.json();
            console.log("Respuesta del servidor:", data);
            const searchResultsContainer = document.getElementById("searchResultsContainer");
            searchResultsContainer.innerHTML = "";

            // Iterar sobre los datos recibidos y crear elementos HTML para cada resultado
            data.forEach(anime => {
                const animeElement = document.createElement("div");
                animeElement.classList.add("anime-item");
                const linkElement = document.createElement("a");
                linkElement.href = `/AnimeDetail?id=${anime.id}`;

                const imgElement = document.createElement("img");
                imgElement.src = anime.posterImage; 
                imgElement.alt = `${anime.title} Poster`;
                imgElement.style.width = "200px";

                const titleElement = document.createElement("div");
                titleElement.style.textAlign = "center";
                titleElement.textContent = anime.title;
                titleElement.style.fontSize = "16px"; 

                linkElement.appendChild(imgElement);
                linkElement.appendChild(titleElement);
                animeElement.appendChild(linkElement);

                // Añade más elementos HTML según sea necesario para mostrar la información de cada anime

                // Agregar el elemento del anime al contenedor de resultados
                searchResultsContainer.appendChild(animeElement);
            });
           
            window.location.href = "#animeSection" ; 

            //window.location.href = "/SearchResults"; // Redireccionar al usuario a la página de resultados de búsqueda
        } else {
            const data = await response.json();
            const errorMessageElement = document.getElementById("errorMessage");
            if (errorMessageElement) {
                errorMessageElement.innerText = data.error; // Mostrar mensaje de error en caso de fallo
            }
            console.error("Error en la respuesta del servidor:", data);
        }
    } catch (error) {
        console.error("Error en la solicitud fetch:", error);
    }
});