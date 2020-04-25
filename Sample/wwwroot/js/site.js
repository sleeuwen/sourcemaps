// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

window.addEventListener('error', function (event) {
    document.querySelector('#stacktrace').textContent = `Original stack trace:\n${event.error.stack}\n\n`;

    fetch('/retrace/retrace', {
        method: 'POST',
        body: event.error.stack,
        headers: {
            'Content-Type': 'text/plain',
        }
    }).then((res) => res.text())
        .then((res) => document.querySelector('#stacktrace').textContent += `ReTraced stack trace:\n${res}`);
});

// Write your JavaScript code.
throwErr();
