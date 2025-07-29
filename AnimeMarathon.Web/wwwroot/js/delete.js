document.getElementById('deleteAccountBtn').addEventListener('click', async function (event) {
    event.preventDefault();

    const confirmDelete = confirm("¿Estás seguro de que deseas eliminar tu cuenta?");
    if (confirmDelete) {
        const userId = this.getAttribute('data-userid');
        const response = await fetch(`https://localhost:7269/User?userId=${userId}`, {
            method: 'DELETE'
        });

        if (response.ok) {
            // Si la cuenta se elimina correctamente, invoca el formulario de cierre de sesión
            document.getElementById('logoutForm').submit();
        } else {
            console.error("Error al eliminar la cuenta");
            alert("Error al eliminar la cuenta. Por favor, inténtelo de nuevo.");
        }
    }
});

