document.addEventListener('DOMContentLoaded', function () {
    (function () {
        var currentPath = window.location.pathname.toLowerCase();
        var navLinks = document.querySelectorAll('.sa-navbar .nav-link');

        navLinks.forEach(function (link) {
            var href = link.getAttribute('href');
            if (!href) return;

            var linkPath = href.toLowerCase();

            if (linkPath === '/' && (currentPath === '/' || currentPath === '/home' || currentPath === '/home/index')) {
                link.classList.add('active');
            }
            else if (linkPath !== '/' && currentPath.startsWith(linkPath.replace('/index', ''))) {
                link.classList.add('active');
            }
        });
    })();
    (function () {
        var alerts = document.querySelectorAll('.sa-alert');
        alerts.forEach(function (alert) {
            setTimeout(function () {
                alert.style.transition = 'opacity 300ms ease, transform 300ms ease';
                alert.style.opacity = '0';
                alert.style.transform = 'translateY(-8px)';
                setTimeout(function () {
                    alert.remove();
                }, 300);
            }, 5000);
        });
    })();

    (function () {
        var searchInputs = document.querySelectorAll('[data-table-search]');

        searchInputs.forEach(function (input) {
            var tableId = input.getAttribute('data-table-search');
            var table = document.getElementById(tableId);
            if (!table) return;

            var tbody = table.querySelector('tbody');
            if (!tbody) return;

            input.addEventListener('input', function () {
                var query = this.value.toLowerCase().trim();
                var rows = tbody.querySelectorAll('tr');
                var visibleCount = 0;

                rows.forEach(function (row) {
                    if (row.classList.contains('no-results-row')) return;

                    var text = row.textContent.toLowerCase();
                    var match = !query || text.indexOf(query) !== -1;
                    row.style.display = match ? '' : 'none';
                    if (match) visibleCount++;
                });

                var noResults = tbody.querySelector('.no-results-row');
                if (!noResults) {
                    noResults = document.createElement('tr');
                    noResults.className = 'no-results-row';
                    var td = document.createElement('td');
                    td.setAttribute('colspan', '99');
                    td.style.textAlign = 'center';
                    td.style.padding = '32px 16px';
                    td.style.color = 'var(--fg-muted)';
                    td.style.fontSize = '0.875rem';
                    td.innerHTML = '<i class="bi bi-search" style="font-size:1.25rem; display:block; margin-bottom:8px;"></i>No se encontraron resultados';
                    noResults.appendChild(td);
                    tbody.appendChild(noResults);
                }

                if (visibleCount === 0 && query) {
                    noResults.style.display = '';
                } else {
                    noResults.style.display = 'none';
                }
            });
        });
    })();

    (function () {
        var notaInputs = document.querySelectorAll('.nota-input');
        if (notaInputs.length === 0) return;

        var preview = document.getElementById('gradePreview');
        var previewValue = document.getElementById('previewValue');
        var previewStatus = document.getElementById('previewStatus');
        if (!preview || !previewValue || !previewStatus) return;

        function calculate() {
            var inputs = document.querySelectorAll('.nota-input');
            var values = [];
            var allFilled = true;

            inputs.forEach(function (input) {
                var val = parseFloat(input.value);
                if (isNaN(val)) {
                    allFilled = false;
                    values.push(0);
                } else {
                    values.push(val);
                }
            });

            var anyFilled = inputs.length > 0 && Array.from(inputs).some(function (i) { return i.value !== ''; });

            if (anyFilled) {
                preview.style.display = '';
                var promedio = (values[0] * 0.3) + (values[1] * 0.3) + (values[2] * 0.4);
                promedio = Math.round(promedio * 100) / 100;

                previewValue.textContent = promedio.toFixed(2);

                if (promedio >= 7) {
                    previewValue.style.color = 'var(--color-success)';
                    previewStatus.className = 'badge-status approved';
                    previewStatus.innerHTML = '<i class="bi bi-check-circle-fill"></i> Aprobado';
                } else {
                    previewValue.style.color = 'var(--color-danger)';
                    previewStatus.className = 'badge-status failed';
                    previewStatus.innerHTML = '<i class="bi bi-x-circle-fill"></i> Reprobado';
                }
            } else {
                if (!previewValue.textContent || previewValue.textContent === '0.00') {
                    preview.style.display = 'none';
                }
            }
        }

        notaInputs.forEach(function (input) {
            input.addEventListener('input', calculate);
            input.addEventListener('change', calculate);
        });

        calculate();
    })();

});
