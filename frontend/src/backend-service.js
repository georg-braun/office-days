import axios from "axios";
import {employeeStore} from "./store.js";

export async function refreshEmployees(){
    try {
        const response = await axios.get("http://192.168.178.85:8001/get-all-employees")        
        response.data;
        employeeStore.set(response.data);

    } catch (error) {
        console.log(error);
    }
}

export async function addEmployee(name){
    try {
        console.log(`Add new employee ${name}`)
        await axios.post(`http://192.168.178.85:8001/add-employee?name=${name}`, {headers: {'content-type': 'application/json'}})        
        await refreshEmployees();
    } catch (error) {
        console.log(error);
    }
}

export async function visitOffice(id, date){
    try {
        const datestring = `${date.getFullYear()}-${date.getMonth()+1}-${date.getDate()}`
        console.log(`${id} visits at ${date}`)
        await axios.post(`http://192.168.178.85:8001/set-at-office?id=${id}&date=${datestring}`, {headers: {'content-type': 'application/json'}})        
        await refreshEmployees();
    } catch (error) {
        console.log(error);
    }
}

export async function stayHome(id, date){
    try {
        const datestring = `${date.getFullYear()}-${date.getMonth()+1}-${date.getDate()}`
        console.log(`${id} visits at ${date}`)
        await axios.post(`http://192.168.178.85:8001/set-at-home?id=${id}&date=${datestring}`, {headers: {'content-type': 'application/json'}})        
        await refreshEmployees();
    } catch (error) {
        console.log(error);
    }
}

export async function deleteEmployee(id){
    try {
        console.log(`delete ${id}`)
        await axios.get(`http://192.168.178.85:8001/delete-employee?id=${id}`, {headers: {'content-type': 'application/json'}})        
        await refreshEmployees();
    } catch (error) {
        console.log(error);
    }
}