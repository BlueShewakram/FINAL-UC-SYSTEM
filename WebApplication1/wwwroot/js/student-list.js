document.addEventListener('DOMContentLoaded', function() {
    const searchInput = document.getElementById('searchInput');
    const table = document.querySelector('.data-table');
    const tbody = document.getElementById('studentTableBody');
    const paginationNav = document.getElementById('studentPaginationList');
    const prevBtn = document.getElementById('studentPrev');
    const nextBtn = document.getElementById('studentNext');
    
    const rowsPerPage = 5;
    let currentPage = 1;
    let allRows = [];
    let filteredRows = [];

    function initialize() {
        if (!tbody) return;
        allRows = Array.from(tbody.querySelectorAll('tr.data-row'));
        filteredRows = [...allRows];
        renderPage();
    }

    function filterRows() {
        const searchText = searchInput.value.toLowerCase().trim();
        if (!searchText) {
            filteredRows = [...allRows];
        } else {
            filteredRows = allRows.filter(row => {
                const text = row.textContent.toLowerCase();
                return text.includes(searchText);
            });
        }
        currentPage = 1;
        renderPage();
    }

    function renderPage() {
        if (!tbody || filteredRows.length === 0) {
            if (allRows.length > 0) {
                allRows.forEach(row => row.style.display = 'none');
            }
            updatePagination(0);
            return;
        }

        const totalPages = Math.ceil(filteredRows.length / rowsPerPage);
        if (currentPage > totalPages) currentPage = totalPages;
        if (currentPage < 1) currentPage = 1;

        const start = (currentPage - 1) * rowsPerPage;
        const end = start + rowsPerPage;
        const pageRows = filteredRows.slice(start, end);

        // Hide all rows first
        allRows.forEach(row => {
            row.style.display = 'none';
            row.style.opacity = '0';
        });

        // Show current page rows with fade in
        setTimeout(() => {
            pageRows.forEach(row => {
                row.style.display = 'table-row';
                row.style.transition = 'opacity 0.3s ease';
                setTimeout(() => {
                    row.style.opacity = '1';
                }, 10);
            });
        }, 50);

        updatePagination(totalPages);
    }

    function updatePagination(totalPages) {
        if (!paginationNav) return;

        // Show/hide pagination
        if (totalPages <= 1) {
            paginationNav.style.display = 'none';
            return;
        } else {
            paginationNav.style.display = 'flex';
        }

        // Update prev/next buttons
        if (prevBtn) {
            prevBtn.disabled = currentPage <= 1;
        }
        if (nextBtn) {
            nextBtn.disabled = currentPage >= totalPages;
        }

        // Remove old page number buttons
        const oldPageBtns = paginationNav.querySelectorAll('.page-btn[data-page]');
        oldPageBtns.forEach(btn => btn.remove());

        // Create new page number buttons
        for (let i = 1; i <= totalPages; i++) {
            const btn = document.createElement('button');
            btn.type = 'button';
            btn.className = 'page-btn' + (i === currentPage ? ' active' : '');
            btn.textContent = i;
            btn.dataset.page = i;
            btn.addEventListener('click', function() {
                currentPage = parseInt(this.dataset.page);
                renderPage();
            });

            // Insert before next button
            if (nextBtn) {
                paginationNav.insertBefore(btn, nextBtn);
            } else {
                paginationNav.appendChild(btn);
            }
        }
    }

    // Event listeners
    if (searchInput) {
        searchInput.addEventListener('input', filterRows);
    }

    if (prevBtn) {
        prevBtn.addEventListener('click', function() {
            if (currentPage > 1) {
                currentPage--;
                renderPage();
            }
        });
    }

    if (nextBtn) {
        nextBtn.addEventListener('click', function() {
            const totalPages = Math.ceil(filteredRows.length / rowsPerPage);
            if (currentPage < totalPages) {
                currentPage++;
                renderPage();
            }
        });
    }

    // Initialize
    initialize();
});
