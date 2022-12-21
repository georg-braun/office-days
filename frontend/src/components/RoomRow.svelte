<script>
	import { deleteRoom, uploadImage } from '../backend-service';
	import AddTable from './AddTable.svelte';
	import TableRow from './TableRow.svelte';

	let fileinput;
	export let timelineStart;
	export let room;

	const onFileSelected = (e) => {
		let image = e.target.files[0];
		let reader = new FileReader();
		console.log(e);
		reader.readAsDataURL(image);

		reader.onloadend = (e) => {
			const imageBase64 = e.target.result;
			uploadImage(room.id, imageBase64);
		};
	};
</script>

<div class="roomRow">
	<div class="roomHeader">
		<div class="roomName">
			{room.name}
		</div>

		<div>
			<input
				style="display:none"
				type="file"
				accept=".jpg, .jpeg, .png"
				on:change={(e) => onFileSelected(e)}
				bind:this={fileinput}
			/>
			{#if room.image}
				<img
					src={room.image}
					alt="room overview"
					class="room-image"
					on:click={() => {
						fileinput.click();
					}}
				/>
			{:else}
				<button
					on:click={() => {
						fileinput.click();
					}}>Upload image</button
				>
			{/if}
		</div>
		<button
			style="margin-left: 5px; border: none; background: transparent;"
			on:click={() => {
				if (confirm('Willst du den Raum wirklich löschen?')) deleteRoom(room.id);
			}}>🗑️</button
		>
	</div>
	<div>
		{#each room.tables as table}
			<TableRow {table} roomId={room.id} {timelineStart} />
		{/each}
	</div>
	<div class="add-table">
		<AddTable roomId={room.id} />
	</div>
</div>

<style>
	.room-image {
		max-height: 50px;
		transition: 0.3s;
	}
	.room-image:hover {
		max-height: 400px;
		transition: 0.3s;
	}
	.roomHeader {
		display: flex;
		align-items: center;
		background-color: skyblue;
		width: 100vw;
		padding: 10px 0px;
	}
	.roomName {
		margin-left: 5px;
		margin-right: 20px;
		font-size: larger;
	}
	.roomRow {
		margin-top: 10px;
	}

	.add-table{
		margin-top: 5px;
	}
</style>
