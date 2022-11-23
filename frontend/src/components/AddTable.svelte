<script>
	import { addTable } from '../backend-service';

	let name = '';
	export let roomId;

	async function addNewTable() {
		try {
			await addTable(roomId, name);
		} catch (error) {
			alert(error.message);
		} finally {
		}
	}
</script>

<input type="text" placeholder="Tisch Bezeichnung" bind:value={name} />
<button class="button block" on:click={addNewTable}>+</button>
