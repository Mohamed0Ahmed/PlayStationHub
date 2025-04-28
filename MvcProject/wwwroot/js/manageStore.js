$(document).ready(function () {
    const storeId = $('#storeId').val();
    let currentBranchId = null;

    // Load Branches, Deleted Branches, Deleted Rooms, and Deleted Guests on page load
    loadBranches();

    // Load Branches as Tabs
    function loadBranches() {
        $.ajax({
            url: `/Admin/GetBranches?storeId=${storeId}`,
            method: 'GET',
            success: function (branches) {
                $('#branchesTabs').empty();
                $('#branchesTabContent').empty();
                if (branches.length === 0) {
                    $('#branchesTabs').append('<li>No branches available.</li>');
                    $('#roomsTable tbody').empty();
                    loadDeletedBranches();
                    loadDeletedRooms(null);
                    loadDeletedGuests(null);
                    return;
                }
                branches.forEach((branch, index) => {
                    const isActive = index === 0 ? 'active' : '';
                    $('#branchesTabs').append(`
                        <li class="nav-item">
                            <a class="nav-link ${isActive}" id="branch-${branch.id}-tab" data-bs-toggle="tab" href="#branch-${branch.id}" role="tab" aria-controls="branch-${branch.id}" aria-selected="${index === 0}">
                                ${branch.branchName}
                                <button type="button" class="btn btn-sm btn-warning ml-2 edit-branch-btn" data-branch-id="${branch.id}" data-branch-name="${branch.branchName}">Edit</button>
                                <button type="button" class="btn btn-sm btn-danger ml-2 delete-branch-btn" data-branch-id="${branch.id}">Delete</button>
                            </a>
                        </li>
                    `);
                    $('#branchesTabContent').append(`
                        <div class="tab-pane fade ${isActive ? 'show active' : ''}" id="branch-${branch.id}" role="tabpanel" aria-labelledby="branch-${branch.id}-tab"></div>
                    `);
                    if (index === 0) {
                        currentBranchId = branch.id;
                        loadRooms(branch.id);
                        loadDeletedBranches();
                        loadDeletedRooms(branch.id);
                        loadDeletedGuests(branch.id);
                    }
                });
            }
        });
    }

    // Switch Tabs
    $('#branchesTabs').on('shown.bs.tab', 'a[data-bs-toggle="tab"]', function (e) {
        const branchId = $(e.target).attr('id').split('-')[1];
        currentBranchId = branchId;
        loadRooms(branchId);
        loadDeletedRooms(branchId);
        loadDeletedGuests(branchId);
    });

    // Create Branch
    $('#createBranchForm').on('submit', function (e) {
        e.preventDefault();
        const storeId = $('#createBranchStoreId').val();
        const branchName = $('#branchName').val();
        $.ajax({
            url: '/Admin/CreateBranch',
            method: 'POST',
            data: { storeId, branchName },
            success: function (response) {
                if (response.success) {
                    $('#createBranchModal').modal('hide');
                    $('#branchName').val('');
                    loadBranches();
                } else {
                    alert('Failed to create branch: ' + response.message);
                }
            }
        });
    });

    // Edit Branch
    $(document).on('click', '.edit-branch-btn', function () {
        const branchId = $(this).data('branch-id');
        const branchName = $(this).data('branch-name');
        $('#editBranchId').val(branchId);
        $('#editBranchName').val(branchName);
        $('#editBranchModal').modal('show');
    });

    $('#editBranchForm').on('submit', function (e) {
        e.preventDefault();
        const id = $('#editBranchId').val();
        const branchName = $('#editBranchName').val();
        $.ajax({
            url: '/Admin/EditBranch',
            method: 'POST',
            data: { id, branchName },
            success: function (response) {
                if (response.success) {
                    $('#editBranchModal').modal('hide');
                    loadBranches();
                } else {
                    alert('Failed to update branch: ' + response.message);
                }
            }
        });
    });

    // Delete Branch
    $(document).on('click', '.delete-branch-btn', function () {
        if (!confirm('Are you sure you want to delete this branch?')) return;
        const branchId = $(this).data('branch-id');
        $.ajax({
            url: '/Admin/DeleteBranch',
            method: 'POST',
            data: { id: branchId },
            success: function (response) {
                if (response.success) {
                    loadBranches();
                    loadDeletedBranches();
                } else {
                    alert('Failed to delete branch: ' + response.message);
                }
            }
        });
    });

    // Load Deleted Branches
    function loadDeletedBranches() {
        $.ajax({
            url: `/Admin/GetDeletedBranches?storeId=${storeId}`,
            method: 'GET',
            success: function (branches) {
                $('#deletedBranchesTable tbody').empty();
                branches.forEach(branch => {
                    $('#deletedBranchesTable tbody').append(`
                        <tr data-branch-id="${branch.id}">
                            <td>${branch.id}</td>
                            <td>${branch.name}</td>
                            <td>
                                <button type="button" class="btn btn-success btn-sm restore-branch-btn" data-branch-id="${branch.id}">Restore</button>
                            </td>
                        </tr>
                    `);
                });
            }
        });
    }

    // Restore Branch
    $(document).on('click', '.restore-branch-btn', function () {
        const branchId = $(this).data('branch-id');
        $('#restoreBranchId').val(branchId);
        $('#restoreBranchModal').modal('show');
    });

    $('#confirm-restore-branch-btn').on('click', function () {
        const branchId = $('#restoreBranchId').val();
        $.ajax({
            url: '/Admin/RestoreBranch',
            method: 'POST',
            data: { id: branchId },
            success: function (response) {
                if (response.success) {
                    $('#restoreBranchModal').modal('hide');
                    loadBranches();
                    loadDeletedBranches();
                } else {
                    alert('Failed to restore branch: ' + response.message);
                }
            }
        });
    });

    // Load Rooms
    function loadRooms(branchId) {
        $.ajax({
            url: `/Admin/GetRooms?branchId=${branchId}`,
            method: 'GET',
            success: function (rooms) {
                $('#roomsTable tbody').empty();
                rooms.forEach(room => {
                    const hasGuest = room.hasGuest ? 'Yes' : 'No';
                    const guestAction = room.hasGuest
                        ? `<button type="button" class="btn btn-danger btn-sm delete-guest-btn" data-room-id="${room.id}">Delete Guest</button>`
                        : `<button type="button" class="btn btn-primary btn-sm add-guest-btn" data-room-id="${room.id}">Add Guest</button>`;
                    $('#roomsTable tbody').append(`
                        <tr data-room-id="${room.id}">
                            <td>${room.id}</td>
                            <td>${room.roomName}</td>
                            <td>${hasGuest}</td>
                            <td>
                                <button type="button" class="btn btn-warning btn-sm edit-room-btn" data-room-id="${room.id}" data-room-name="${room.roomName}">Edit</button>
                                <button type="button" class="btn btn-danger btn-sm delete-room-btn" data-room-id="${room.id}">Delete</button>
                                ${guestAction}
                            </td>
                        </tr>
                    `);
                });
            }
        });
    }

    // Create Room
    $('#createRoomForm').on('submit', function (e) {
        e.preventDefault();
        const branchId = $('#createRoomBranchId').val();
        const roomName = $('#roomName').val();
        $.ajax({
            url: '/Admin/CreateRoom',
            method: 'POST',
            data: { branchId, roomName },
            success: function (response) {
                if (response.success) {
                    $('#createRoomModal').modal('hide');
                    $('#roomName').val('');
                    loadRooms(branchId);
                } else {
                    alert('Failed to create room: ' + response.message);
                }
            }
        });
    });

    // Edit Room
    $(document).on('click', '.edit-room-btn', function () {
        const roomId = $(this).data('room-id');
        const roomName = $(this).data('room-name');
        $('#editRoomId').val(roomId);
        $('#editRoomName').val(roomName);
        $('#editRoomModal').modal('show');
    });

    $('#editRoomForm').on('submit', function (e) {
        e.preventDefault();
        const id = $('#editRoomId').val();
        const roomName = $('#editRoomName').val();
        $.ajax({
            url: '/Admin/EditRoom',
            method: 'POST',
            data: { id, roomName },
            success: function (response) {
                if (response.success) {
                    $('#editRoomModal').modal('hide');
                    loadRooms(currentBranchId);
                } else {
                    alert('Failed to update room: ' + response.message);
                }
            }
        });
    });

    // Delete Room
    $(document).on('click', '.delete-room-btn', function () {
        if (!confirm('Are you sure you want to delete this room?')) return;
        const roomId = $(this).data('room-id');
        $.ajax({
            url: '/Admin/DeleteRoom',
            method: 'POST',
            data: { id: roomId },
            success: function (response) {
                if (response.success) {
                    loadRooms(currentBranchId);
                    loadDeletedRooms(currentBranchId);
                } else {
                    alert('Failed to delete room: ' + response.message);
                }
            }
        });
    });

    // Load Deleted Rooms
    function loadDeletedRooms(branchId) {
        if (!branchId) {
            $('#deletedRoomsTable tbody').empty();
            return;
        }
        $.ajax({
            url: `/Admin/GetDeletedRooms?branchId=${branchId}`,
            method: 'GET',
            success: function (rooms) {
                $('#deletedRoomsTable tbody').empty();
                rooms.forEach(room => {
                    $('#deletedRoomsTable tbody').append(`
                        <tr data-room-id="${room.id}">
                            <td>${room.id}</td>
                            <td>${room.name}</td>
                            <td>
                                <button type="button" class="btn btn-success btn-sm restore-room-btn" data-room-id="${room.id}">Restore</button>
                            </td>
                        </tr>
                    `);
                });
            }
        });
    }

    // Restore Room
    $(document).on('click', '.restore-room-btn', function () {
        const roomId = $(this).data('room-id');
        $('#restoreRoomId').val(roomId);
        $('#restoreRoomModal').modal('show');
    });

    $('#confirm-restore-room-btn').on('click', function () {
        const roomId = $('#restoreRoomId').val();
        $.ajax({
            url: '/Admin/RestoreRoom',
            method: 'POST',
            data: { id: roomId },
            success: function (response) {
                if (response.success) {
                    $('#restoreRoomModal').modal('hide');
                    loadRooms(currentBranchId);
                    loadDeletedRooms(currentBranchId);
                } else {
                    alert('Failed to restore room: ' + response.message);
                }
            }
        });
    });

    // Add Guest
    $(document).on('click', '.add-guest-btn', function () {
        const roomId = $(this).data('room-id');
        $('#createGuestRoomId').val(roomId);
        $('#createGuestModal').modal('show');
    });

    $('#createGuestForm').on('submit', function (e) {
        e.preventDefault();
        const roomId = $('#createGuestRoomId').val();
        const storeId = $('#createGuestStoreId').val();
        const username = $('#guestUsername').val();
        const password = $('#guestPassword').val();
        $.ajax({
            url: '/Admin/CreateGuest',
            method: 'POST',
            data: { roomId, storeId, username, password },
            success: function (response) {
                if (response.success) {
                    $('#createGuestModal').modal('hide');
                    $('#guestUsername').val('');
                    $('#guestPassword').val('');
                    loadRooms(currentBranchId);
                } else {
                    alert('Failed to create guest: ' + response.message);
                }
            }
        });
    });

    // Delete Guest
    $(document).on('click', '.delete-guest-btn', function () {
        if (!confirm('Are you sure you want to delete this guest?')) return;
        const roomId = $(this).data('room-id');
        $.ajax({
            url: `/Admin/GetGuests?roomId=${roomId}`,
            method: 'GET',
            success: function (guests) {
                if (guests.length > 0) {
                    const guestId = guests[0].id;
                    $.ajax({
                        url: '/Admin/DeleteGuest',
                        method: 'POST',
                        data: { id: guestId },
                        success: function (response) {
                            if (response.success) {
                                loadRooms(currentBranchId);
                                loadDeletedGuests(currentBranchId);
                            } else {
                                alert('Failed to delete guest: ' + response.message);
                            }
                        }
                    });
                }
            }
        });
    });

    // Load Deleted Guests
    function loadDeletedGuests(branchId) {
        if (!branchId) {
            $('#deletedGuestsTable tbody').empty();
            return;
        }
        $.ajax({
            url: `/Admin/GetDeletedGuests?branchId=${branchId}`,
            method: 'GET',
            success: function (guests) {
                $('#deletedGuestsTable tbody').empty();
                guests.forEach(guest => {
                    $('#deletedGuestsTable tbody').append(`
                        <tr data-guest-id="${guest.id}">
                            <td>${guest.id}</td>
                            <td>${guest.username}</td>
                            <td>
                                <button type="button" class="btn btn-success btn-sm restore-guest-btn" data-guest-id="${guest.id}">Restore</button>
                            </td>
                        </tr>
                    `);
                });
            }
        });
    }

    // Restore Guest
    $(document).on('click', '.restore-guest-btn', function () {
        const guestId = $(this).data('guest-id');
        $('#restoreGuestId').val(guestId);
        $('#restoreGuestModal').modal('show');
    });

    $('#confirm-restore-guest-btn').on('click', function () {
        const guestId = $('#restoreGuestId').val();
        $.ajax({
            url: '/Admin/RestoreGuest',
            method: 'POST',
            data: { id: guestId },
            success: function (response) {
                if (response.success) {
                    $('#restoreGuestModal').modal('hide');
                    loadRooms(currentBranchId);
                    loadDeletedGuests(currentBranchId);
                } else {
                    alert('Failed to restore guest: ' + response.message);
                }
            }
        });
    });

    // Set branchId for Create Room Modal
    $(document).on('click', '[data-bs-target="#createRoomModal"]', function () {
        if (!currentBranchId) {
            alert('Please select a branch first.');
            return;
        }
        $('#createRoomBranchId').val(currentBranchId);
    });
});