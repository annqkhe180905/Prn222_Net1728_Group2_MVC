

document.addEventListener('DOMContentLoaded', function () {
    const categoryForm = document.querySelector('#categoryForm');

    categoryForm.addEventListener('submit', async (e) => {
        e.preventDefault();
        const formData = new FormData(categoryForm);
        try {
            const response = await fetch('/Category/Create', {
                method: 'POST',
                body: formData,
            });

            if (response.ok) {
                location.reload();
            }
        } catch (error) {
            console.log(error);
        }
    })

    document.querySelectorAll(".deleteCategoryButton").forEach(item => {
        item.addEventListener('click', async () => {
            const id = item.getAttribute('data-id');
            if (!confirm('Are you sure you want to delete this category?')) {
                return;
            }
            try {
                const response = await fetch(`/Category/Remove?id=${id}`, {
                    method: 'GET',
                })
                if (response.ok) {
                    const category = item.closest('.category-item');
                    category.remove();
                }
            } catch (error) {
                console.log(error);
            }
        })
    })

    document.getElementById('editCategory').addEventListener('show.bs.modal', function (e) {
        const button = e.relatedTarget;

        const categoryId = button.getAttribute('data-id');
        const categoryName = button.getAttribute('data-name');
        const categoryDescription = button.getAttribute('data-description');
        const isActive = button.getAttribute('data-isActive');

        this.querySelector('#editCateId').value = categoryId;
        this.querySelector('#editCateName').value = categoryName;
        this.querySelector('#editCateDescription').value = categoryDescription;
        const select = this.querySelector('.editSelect');
        select.value = isActive;

        const saveButton = this.querySelector('#editCategoryButton');
        const form = this.querySelector('#categoryEditForm');

        saveButton.addEventListener('click', async (e) => {
            e.preventDefault();
            const formData = new FormData(form);
            try {
                const response = await fetch('/Category/Edit', {
                    method: 'POST',
                    body: formData
                });

                if (response.ok) {
                    location.reload();
                }

            } catch (error) {
                console.log(error);
            }
        })
    })
})