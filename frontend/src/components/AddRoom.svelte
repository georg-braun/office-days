<script>
	import { addRoom } from '../backend-service';

	let name = '';

	async function addNewRoom() {
		try {
			await addRoom(name);
		} catch (error) {
			alert(error.message);
		} finally {
		}
	}
</script>

<input type="text" placeholder="Raum" bind:value={name} />
<button class="button block" on:click={addNewRoom}> Hinzufügen </button>
