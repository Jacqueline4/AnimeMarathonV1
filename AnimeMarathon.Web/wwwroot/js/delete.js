document.getElementById('deleteAccountBtn').addEventListener('click', async function (event) {
    event.preventDefault();

    const confirmDelete = confirm("¿Estás seguro de que deseas eliminar tu cuenta?");
    if (confirmDelete) {
        const userId = this.getAttribute('data-userid');
        const response = await fetch(`https://localhost:7269/User?userId=${userId}`, {
            method: 'DELETE'
        });

        if (response.ok) {
            // Cerrar sesión después de eliminar la cuenta
            const logoutResponse = await fetch("/Logout", {
                method: 'POST'
            });

            if (logoutResponse.ok) {
                // Redirigir al usuario a la página de inicio u otra página
                window.location.href = "/";
            } else {
                // Manejar el caso de error al cerrar sesión
                console.error("Error al cerrar sesión");
            }
        } else {
            // Manejar el caso de error al eliminar la cuenta
            console.error("Error al eliminar la cuenta");
        }
    }
});
