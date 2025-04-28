$(document).ready(function () {
    // Load Owners and Deleted Stores/Owners on page load
    loadOwners();
    loadDeletedStores();
    loadDeletedOwners();

    // Create Store
    $('#createStoreForm').on('submit', function (e) {
        e.preventDefault();
        const storeName = $('#storeName').val();
        const address = $('#storeAddress').val();
        $.ajax({
            url: '/Admin/CreateStore',
            method: 'POST',
            data: { storeName, address },
            success: function (response) {
                if (response.success) {
                    $('#createStoreModal').modal('hide');
                    $('#storesTable tbody').append(`
                        <tr data-store-id="${response.storeId}">
                            <td>${response.storeId}</td>
                            <td>${response.storeName}</td>
                            <td>${address}</td>
                            <td>
                                <a href="/Admin/ManageStore/${response.storeId}" class="btn btn-warning btn-sm edit-store-btn">Edit</a>
                                <button type="button" class="btn btn-danger btn-sm delete-store-btn" data-store-id="${response.storeId}">Delete</button>
                            </td>
                        </tr>
                    `);
                    $('#storeName').val('');
                    $('#storeAddress').val('');
                    // Update the store dropdown in the Create Owner modal
                    $('#ownerStoreId').append(`<option value="${response.storeId}">${response.storeName}</option>`);
                } else {
                    alert('Failed to create store: ' + response.message);
                }
            }
        });
    });

    // Edit Store
    $(document).on('click', '.edit-store-btn', function (e) {
        // The Edit button now redirects to ManageStore.cshtml, so no JavaScript handling needed here
        // The href is set in the HTML to "/Admin/ManageStore/{storeId}"
    });

    $('#editStoreForm').on('submit', function (e) {
        e.preventDefault();
        const id = $('#editStoreId').val();
        const storeName = $('#editStoreName').val();
        const address = $('#editStoreAddress').val();
        $.ajax({
            url: '/Admin/EditStore',
            method: 'POST',
            data: { id, storeName, address },
            success: function (response) {
                if (response.success) {
                    $('#editStoreModal').modal('hide');
                    $(`#storesTable tr[data-store-id="${id}"] td:nth-child(2)`).text(storeName);
                    $(`#storesTable tr[data-store-id="${id}"] td:nth-child(3)`).text(address);
                    // Update the store dropdown in the Create Owner modal
                    $(`#ownerStoreId option[value="${id}"]`).text(storeName);
                } else {
                    alert('Failed to update store: ' + response.message);
                }
            }
        });
    });

    // Delete Store
    $(document).on('click', '.delete-store-btn', function () {
        if (!confirm('Are you sure you want to delete this store?')) return;
        const storeId = $(this).data('store-id');
        $.ajax({
            url: '/Admin/DeleteStore',
            method: 'POST',
            data: { id: storeId },
            success: function (response) {
                if (response.success) {
                    $(`#storesTable tr[data-store-id="${storeId}"]`).remove();
                } else {
                    alert('Failed to delete store: ' + response.message);
                }
            }
        });
    });

    // Load Owners
    function loadOwners() {
        $.ajax({
            url: '/Admin/GetOwners',
            method: 'GET',
            success: function (owners) {
                $('#ownersTable tbody').empty();
                owners.forEach(owner => {
                    $('#ownersTable tbody').append(`
                        <tr data-owner-id="${owner.id}">
                            <td>${owner.id}</td>
                            <td>${owner.email}</td>
                            <td>
                                <button type="button" class="btn btn-warning btn-sm edit-owner-btn" data-owner-id="${owner.id}" data-owner-email="${owner.email}">Edit</button>
                                <button type="button" class="btn btn-danger btn-sm delete-owner-btn" data-owner-id="${owner.id}">Delete</button>
                            </td>
                        </tr>
                    `);
                });
            }
        });
    }

    // Create Owner
    $('#createOwnerForm').on('submit', function (e) {
        e.preventDefault();
        const email = $('#ownerEmail').val();
        const password = $('#ownerPassword').val();
        const storeId = $('#ownerStoreId').val();
        $.ajax({
            url: '/Admin/CreateOwner',
            method: 'POST',
            data: { email, password, storeId },
            success: function (response) {
                if (response.success) {
                    $('#createOwnerModal').modal('hide');
                    $('#ownersTable tbody').append(`
                        <tr data-owner-id="${response.ownerId}">
                            <td>${response.ownerId}</td>
                            <td>${response.email}</td>
                            <td>
                                <button type="button" class="btn btn-warning btn-sm edit-owner-btn" data-owner-id="${response.ownerId}" data-owner-email="${response.email}">Edit</button>
                                <button type="button" class="btn btn-danger btn-sm delete-owner-btn" data-owner-id="${response.ownerId}">Delete</button>
                            </td>
                        </tr>
                    `);
                    $('#ownerEmail').val('');
                    $('#ownerPassword').val('');
                    $('#ownerStoreId').val('');
                } else {
                    alert('Failed to create owner: ' + response.message);
                }
            }
        });
    });

    // Edit Owner
    $(document).on('click', '.edit-owner-btn', function () {
        const ownerId = $(this).data('owner-id');
        const email = $(this).data('owner-email');
        $('#editOwnerId').val(ownerId);
        $('#editOwnerEmail').val(email);
        $('#editOwnerModal').modal('show');
    });

    $('#editOwnerForm').on('submit', function (e) {
        e.preventDefault();
        const id = $('#editOwnerId').val();
        const email = $('#editOwnerEmail').val();
        $.ajax({
            url: '/Admin/EditOwner',
            method: 'POST',
            data: { id, email },
            success: function (response) {
                if (response.success) {
                    $('#editOwnerModal').modal('hide');
                    const row = $(`#ownersTable tr[data-owner-id="${id}"]`);
                    row.find('td:nth-child(2)').text(email);
                    row.find('.edit-owner-btn').data('owner-email', email);
                } else {
                    alert('Failed to update owner: ' + response.message);
                }
            }
        });
    });

    // Delete Owner
    $(document).on('click', '.delete-owner-btn', function () {
        if (!confirm('Are you sure you want to delete this owner?')) return;
        const ownerId = $(this).data('owner-id');
        $.ajax({
            url: '/Admin/DeleteOwner',
            method: 'POST',
            data: { id: ownerId },
            success: function (response) {
                if (response.success) {
                    $(`#ownersTable tr[data-owner-id="${ownerId}"]`).remove();
                    loadDeletedOwners(); // Reload deleted owners table
                } else {
                    alert('Failed to delete owner: ' + response.message);
                }
            }
        });
    });

    // Load Deleted Stores
    function loadDeletedStores() {
        $.ajax({
            url: '/Admin/GetDeletedStores',
            method: 'GET',
            success: function (stores) {
                $('#deletedStoresTable tbody').empty();
                stores.forEach(store => {
                    $('#deletedStoresTable tbody').append(`
                        <tr data-store-id="${store.id}">
                            <td>${store.id}</td>
                            <td>${store.name}</td>
                            <td>${store.address}</td>
                            <td>
                                <button type="button" class="btn btn-success btn-sm restore-store-btn" data-store-id="${store.id}">Restore</button>
                            </td>
                        </tr>
                    `);
                });
            }
        });
    }

    // Restore Store
    $(document).on('click', '.restore-store-btn', function () {
        if (!confirm('Are you sure you want to restore this store?')) return;
        const storeId = $(this).data('store-id');
        $.ajax({
            url: '/Admin/RestoreStore',
            method: 'POST',
            data: { id: storeId },
            success: function (response) {
                if (response.success) {
                    $(`#deletedStoresTable tr[data-store-id="${storeId}"]`).remove();
                    $('#storesTable tbody').append(`
                        <tr data-store-id="${storeId}">
                            <td>${storeId}</td>
                            <td>${response.storeName}</td>
                            <td>${response.storeAddress}</td>
                            <td>
                                <a href="/Admin/ManageStore/${storeId}" class="btn btn-warning btn-sm edit-store-btn">Edit</a>
                                <button type="button" class="btn btn-danger btn-sm delete-store-btn" data-store-id="${storeId}">Delete</button>
                            </td>
                        </tr>
                    `);
                    // Update the store dropdown in the Create Owner modal
                    $('#ownerStoreId').append(`<option value="${storeId}">${response.storeName}</option>`);
                } else {
                    alert('Failed to restore store: ' + response.message);
                }
            }
        });
    });

    // Load Deleted Owners
    function loadDeletedOwners() {
        $.ajax({
            url: '/Admin/GetDeletedOwners',
            method: 'GET',
            success: function (owners) {
                $('#deletedOwnersTable tbody').empty();
                owners.forEach(owner => {
                    $('#deletedOwnersTable tbody').append(`
                        <tr data-owner-id="${owner.id}">
                            <td>${owner.id}</td>
                            <td>${owner.email}</td>
                            <td>
                                <button type="button" class="btn btn-success btn-sm restore-owner-btn" data-owner-id="${owner.id}" data-owner-email="${owner.email}">Restore</button>
                            </td>
                        </tr>
                    `);
                });
            }
        });
    }

    // Restore Owner
    $(document).on('click', '.restore-owner-btn', function () {
        const ownerId = $(this).data('owner-id');
        $('#restoreOwnerId').val(ownerId);
        $('#restoreOwnerModal').modal('show');
    });

    $('#confirm-restore-owner-btn').on('click', function () {
        const ownerId = $('#restoreOwnerId').val();
        $.ajax({
            url: '/Admin/RestoreOwner',
            method: 'POST',
            data: { id: ownerId },
            success: function (response) {
                if (response.success) {
                    $('#restoreOwnerModal').modal('hide');
                    $(`#deletedOwnersTable tr[data-owner-id="${ownerId}"]`).remove();
                    $('#ownersTable tbody').append(`
                        <tr data-owner-id="${ownerId}">
                            <td>${ownerId}</td>
                            <td>${response.email}</td>
                            <td>
                                <button type="button" class="btn btn-warning btn-sm edit-owner-btn" data-owner-id="${ownerId}" data-owner-email="${response.email}">Edit</button>
                                <button type="button" class="btn btn-danger btn-sm delete-owner-btn" data-owner-id="${ownerId}">Delete</button>
                            </td>
                        </tr>
                    `);
                } else {
                    alert('Failed to restore owner: ' + response.message);
                }
            }
        });
    });
});