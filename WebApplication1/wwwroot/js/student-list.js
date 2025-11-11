document.addEventListener('DOMContentLoaded', function() {
    const searchInput = document.getElementById('searchInput');
    const table = document.querySelector('.custom-table');
    const tbody = table ? table.querySelector('tbody') : null;
    // support either the nav having id 'studentPagination' or the ul having id 'studentPaginationList'
    const paginationNav = document.getElementById('studentPagination') || document.getElementById('studentPaginationList');

    // Only enable client-side pagination on pages that include the studentPagination nav (Index view)
    if (!table || !tbody || !paginationNav) {
        // fallback: only wire up search (if present)
        if (searchInput) {
            searchInput.addEventListener('input', function(e) {
                const searchText = e.target.value.toLowerCase();
                const tableRows = document.querySelectorAll('.custom-table tbody tr');
                tableRows.forEach(row => {
                    const text = row.textContent.toLowerCase();
                    row.style.display = text.includes(searchText) ? '' : 'none';
                });
            });
        }
        return;
    }

    const prevBtn = document.getElementById('studentPrev');
    const nextBtn = document.getElementById('studentNext');
    const paginationList = document.getElementById('studentPaginationList');
    const rowsPerPage = 5;
    let currentPage = 1;

    function getAllRows() {
        return Array.from(tbody.querySelectorAll('tr'));
    }

    function getFilteredRows() {
        // Determine filtered rows based on the current search input value
        const searchText = searchInput ? searchInput.value.toLowerCase().trim() : '';
        if (!searchText) {
            return getAllRows();
        }
        return getAllRows().filter(r => r.textContent.toLowerCase().includes(searchText));
    }

    function renderPage() {
        const filtered = getFilteredRows();
        const totalPages = Math.max(1, Math.ceil(filtered.length / rowsPerPage));
        if (currentPage > totalPages) currentPage = totalPages;

        // If there is 1 or fewer pages, hide the pagination entirely
        if (paginationList) {
            paginationList.style.display = totalPages <= 1 ? 'none' : '';
        }

        // Get rows to show on current page
        const start = (currentPage - 1) * rowsPerPage;
        const pageRows = filtered.slice(start, start + rowsPerPage);
        const allRows = getAllRows();

        // Fade out all rows first
        allRows.forEach(r => {
            if (!pageRows.includes(r)) {
                r.style.opacity = '0';
                r.style.transition = 'opacity 0.2s ease';
                setTimeout(() => {
                    r.style.display = 'none';
                }, 200);
            }
        });

        // Fade in page rows after brief delay
        setTimeout(() => {
            pageRows.forEach(r => {
                r.style.display = 'table-row';
                r.style.opacity = '0';
                r.style.transition = 'opacity 0.3s ease';
                // Force reflow
                r.offsetHeight;
                r.style.opacity = '1';
            });
        }, 250);

        // Disable/enable Prev/Next buttons
        if (prevBtn) prevBtn.disabled = currentPage <= 1;
        if (nextBtn) nextBtn.disabled = currentPage >= totalPages;

        // Rebuild number buttons
        if (paginationList) {
            // remove existing number items with marker class
            const existing = paginationList.querySelectorAll('.student-page-number');
            existing.forEach(n => n.remove());

            // insert page number items before the next button's parent <li>
            const nextLi = nextBtn ? nextBtn.closest('li') : null;

            for (let i = 1; i <= totalPages; i++) {
                const li = document.createElement('li');
                li.className = 'page-item student-page-number' + (i === currentPage ? ' active' : '');

                const btn = document.createElement('button');
                btn.type = 'button';
                btn.className = 'page-link';
                btn.textContent = i;
                btn.dataset.page = i;
                btn.addEventListener('click', function() {
                    currentPage = Number(this.dataset.page);
                    renderPage();
                });

                li.appendChild(btn);

                if (nextLi && nextLi.parentNode) {
                    nextLi.parentNode.insertBefore(li, nextLi);
                } else if (paginationList) {
                    paginationList.appendChild(li);
                }
            }
        }
    }

    // Wire up Prev/Next
    if (prevBtn) prevBtn.addEventListener('click', function() {
        const filtered = getFilteredRows();
        const totalPages = Math.max(1, Math.ceil(filtered.length / rowsPerPage));
        if (currentPage > 1) {
            currentPage--;
            renderPage();
        }
    });

    if (nextBtn) nextBtn.addEventListener('click', function() {
        const filtered = getFilteredRows();
        const totalPages = Math.max(1, Math.ceil(filtered.length / rowsPerPage));
        if (currentPage < totalPages) {
            currentPage++;
            renderPage();
        }
    });

    // Wire up search: reset to page 1 and re-render when search input changes
    if (searchInput) {
        searchInput.addEventListener('input', function(e) {
            currentPage = 1;
            renderPage();
        });
    }

    // Initialize: show first page
    renderPage();
});