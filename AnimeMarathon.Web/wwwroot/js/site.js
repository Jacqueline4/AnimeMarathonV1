// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
document.getElementById("loginForm").addEventListener("submit", async function (event) {
    event.preventDefault();
    // alert("Script ejecutándose");

    const formData = new FormData(event.target);
    const username = formData.get("username");
    const password = formData.get("password");

    const response = fetch("https://localhost:7269/User/Login", {
        method: "POST",
        headers: {
            "Content-Type": "application/json"
        },
        body: JSON.stringify({
            name: username,
            password: password
        })
    }).then(response => {

        if (response.ok) {
            window.location.href = "/UserMenu/" + username; // Redireccionar al usuario a la página principal si la autenticación es exitosa
            sessionStorage.setItem("me", JSON.stringify(response.data));
            /////EN paginas del usuarion
            //var yo = sessionStorage.getItem("me");
            //if (!yo) {
            //    window.location.href = "/Login";
            //}
        } else {
            const data = await response.json();
            document.getElementById("errorMessage").innerText = data.error; // Mostrar mensaje de error en caso de fallo de autenticación
            // console.log("Respuesta del servidor:", data);
        }        
    }).error(err => {
          //Con el throw de back
    });

    

});