document.getElementById('editUserForm').addEventListener('submit',async function (event) {
    event.preventDefault(); // Evitar que el formulario se envíe de la manera tradicional

    const formData = new FormData(this);
    const data = Object.fromEntries(formData.entries());
    
    const response = await fetch("https://localhost:7269/User", {
        method: 'PUT',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(data)
    });


    if (response.ok) {

        window.location.href = "/Login"; 
    } else {
        document.getElementById("errorMessage").innerText = "Usuario o contraseña duplicados";
        const data = await response.json();
        document.getElementById("errorMessage").innerText = data.error;
    }
});
