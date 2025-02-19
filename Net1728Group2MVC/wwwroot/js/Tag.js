document.addEventListener('DOMContentLoaded', function () {
    const tagForm = document.querySelector('#tagForm');
    const editTagForm = document.querySelector('#editTagForm');
    const deleteTagForm = document.querySelector('#deleteTagForm');

    if (tagForm) {
        tagForm.addEventListener('submit', async (e) => {
            e.preventDefault();
            const formData = new FormData(tagForm);
            try {
                const response = await fetch('/NewsTag/Create', {  
                    method: 'POST',
                    body: formData
                });

                if (!response.ok) throw new Error(await response.text());
                location.reload();
            } catch (error) {
                console.error('Create Error:', error);
                alert('Failed to create NewsTag. Check console for details.');
            }
        });
    }

    if (editTagForm) {
        editTagForm.addEventListener('submit', async (e) => {
            e.preventDefault();
            const formData = new FormData(editTagForm);
            try {
                const response = await fetch('/NewsTag/Edit', {  
                    method: 'POST',
                    body: formData
                });

                if (!response.ok) throw new Error(await response.text());
                location.reload();
            } catch (error) {
                console.error('Edit Error:', error);
                alert('Failed to edit NewsTag. Check console for details.');
            }
        });
    }

    if (deleteTagForm) {
        deleteTagForm.addEventListener('submit', async (e) => {
            e.preventDefault();
            const id = document.getElementById('deleteTagId').value;
            try {
                const response = await fetch(`/NewsTag/Delete?id=${id}`, {
                    method: 'POST'
                });

                if (!response.ok) throw new Error(await response.text());
                location.reload();
            } catch (error) {
                console.error('Delete Error:', error);
                alert('Failed to delete NewsTag. Check console for details.');
            }
        });
    }
});

function loadTag(id) {
    fetch(`/NewsTag/GetById?id=${id}`)  
        .then(response => response.json())
        .then(tag => {
            document.getElementById('editTagId').value = tag.tagId;
            document.getElementById('editTagName').value = tag.tagName;
            document.getElementById('editTagNote').value = tag.note;
        })
        .catch(error => console.error('Load NewsTag Error:', error));
}

function setDeleteTagId(id) {
    document.getElementById('deleteTagId').value = id;
}
