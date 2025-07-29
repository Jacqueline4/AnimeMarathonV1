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
        window.location.href = "/Login"; 
    } else {
        document.getElementById("errorMessage").innerText = "Usuario o email duplicados";
        const data = await response.json();
       
    }
});