<script>
	import { addEmployee } from '../backend-service';

	let name = '';

	async function addNewEmployee() {
		try {
			addEmployee(name);
		} catch (error) {
			alert(error.message);
		} finally {
		}
	}
</script>

<input type="text" placeholder="Mitarbeiter" bind:value={name} />
<button class="button block" on:click={addNewEmployee}> Hinzufügen </button>
