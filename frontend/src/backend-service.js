import axios from 'axios';

import { employeeStore } from './store.js';

const server = 'x'; // import.meta.env.VITE_BACKEND_SERVER;

console.log(server);
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
