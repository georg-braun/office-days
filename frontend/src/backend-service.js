import axios from 'axios';

import { employeeStore, roomStore } from './store.js';

const server = import.meta.env.VITE_BACKEND_SERVER;

console.log(server);
export async function refreshRooms() {
	try {
		const response = await axios.get(`${server}/room-utilization`);
		console.log(response.data);
		roomStore.set(response.data);
	} catch (error) {
		console.log(error);
	}
}


export async function refreshEmployees() {
	try {
		const response = await axios.get(`${server}/get-all-employees`);
		response.data;
		employeeStore.set(response.data);
	} catch (error) {
		console.log(error);
	}
}

export async function addEmployee(name) {
	try {
		console.log(`Add new employee ${name}`);
		await axios.post(`${server}/add-employee?name=${name}`, {
			headers: { 'content-type': 'application/json' }
		});
		await refreshEmployees();
	} catch (error) {
		console.log(error);
	}
}


export async function uploadImage(roomId, image) {
	try {

		console.log("Upload image")
		const data = JSON.stringify(image);
		await axios.post(`${server}/update-room-image?roomId=${roomId}`, data, {
			headers: {
				// Overwrite Axios's automatically set Content-Type
				'Content-Type': 'application/json'
			}
		}
		);

	} catch (error) {
		console.log(error);
	}
}

export async function addRoom(name) {
	try {
		console.log(`Add new room ${name}`);
		await axios.post(`${server}/add-room?name=${name}`, {
			headers: { 'content-type': 'application/json' }
		});
		await refreshRooms();
	} catch (error) {
		console.log(error);
	}
}

export async function addTable(roomId, name) {
	try {
		console.log(`Add new room ${name}`);
		await axios.post(`${server}/add-table?roomId=${roomId}&name=${name}`, {
			headers: { 'content-type': 'application/json' }
		});
		await refreshRooms();
	} catch (error) {
		console.log(error);
	}
}

export async function visitOffice(id, date) {
	try {
		const datestring = `${date.getFullYear()}-${date.getMonth() + 1}-${date.getDate()}`;
		console.log(`${id} visits the office at ${date}`);
		await axios.post(`${server}/set-at-office?id=${id}&date=${datestring}`, {
			headers: { 'content-type': 'application/json' }
		});
		await refreshEmployees();
	} catch (error) {
		console.log(error);
	}
}

export async function stayHome(id, date) {
	try {
		const datestring = `${date.getFullYear()}-${date.getMonth() + 1}-${date.getDate()}`;
		console.log(`${id} stays at homt ${date}`);
		await axios.post(`${server}/set-at-home?id=${id}&date=${datestring}`, {
			headers: { 'content-type': 'application/json' }
		});
		await refreshEmployees();
	} catch (error) {
		console.log(error);
	}
}

export async function deleteEmployee(id) {
	try {
		console.log(`delete ${id}`);
		await axios.get(`${server}/delete-employee?id=${id}`, {
			headers: { 'content-type': 'application/json' }
		});
		await refreshEmployees();
	} catch (error) {
		console.log(error);
	}
}

export async function deleteRoom(roomId) {
	try {
		console.log(`delete ${roomId}`);
		await axios.get(`${server}/delete-room?roomId=${roomId}`, {
			headers: { 'content-type': 'application/json' }
		});
		await refreshRooms();
	} catch (error) {
		console.log(error);
	}
}

export async function deleteTable(roomId, tableId) {
	try {
		console.log(`delete ${roomId}`);
		await axios.get(`${server}/delete-table?roomId=${roomId}&tableId=${tableId}`, {
			headers: { 'content-type': 'application/json' }
		});
		await refreshRooms();
	} catch (error) {
		console.log(error);
	}
}


export async function bookTable(roomId, tableId, date, person) {
	try {
		const datestring = `${date.getFullYear()}-${date.getMonth() + 1}-${date.getDate()}`;
		console.log(`${person} reserve a table at ${date}`);
		await axios.post(`${server}/toggle-reservation?roomId=${roomId}&tableId=${tableId}&date=${datestring}&person=${person}`, {
			headers: { 'content-type': 'application/json' }
		});
		await refreshRooms();
	} catch (error) {
		console.log(error);
	}
}