document.getElementById('editUserForm').addEventListener('submit', function(event) {
        event.preventDefault(); // Evitar que el formulario se envíe de la manera tradicional

    // Obtener los datos del formulario
    const formData = new FormData(this);
    const data = Object.fromEntries(formData.entries());

    // Hacer la solicitud POST
    fetch('https://localhost:7269/User', {
        method: 'POST',
    headers: {
        'Content-Type': 'application/json'
        },
    body: JSON.stringify(data)
    })
    .then(response => {
        if (response.ok) {
            return response.json();
        }
    throw new Error('Network response was not ok.');
    })
    .then(data => {
        console.log('Success:', data);
    // Aquí puedes agregar la lógica para manejar una respuesta exitosa
    alert('Los cambios han sido guardados exitosamente.');
    })
    .catch(error => {
        console.error('Error:', error);
    // Aquí puedes agregar la lógica para manejar errores
    alert('Hubo un problema al guardar los cambios.');
    });
});

