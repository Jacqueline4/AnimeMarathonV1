document.getElementById("registerForm").addEventListener("submit", async function (event) {
    event.preventDefault();

    const formData = new FormData(event.target);
    const name = formData.get("name");
    const lastName = formData.get("lastName");
    const email = formData.get("email");
    const password = formData.get("password");

    const response = await fetch("https://localhost:7269/User/Registro", {
        method: "POST",
        headers: {
            "Content-Type": "application/json"
        },
        body: JSON.stringify({
            name: name,
            lastName: lastName,
            email: email,
            password: password
        })
    });

    if (response.ok) {
        window.location.href = "/Login"; // Redireccionar al usuario a la página de inicio de sesión si el registro es exitoso
    } else {
        const data = await response.json();
        document.getElementById("errorMessage").innerText = data.error; // Mostrar mensaje de error en caso de fallo de registro
    }
});