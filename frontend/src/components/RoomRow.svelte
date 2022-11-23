<script>
	import { deleteRoom } from '../backend-service';
	import AddTable from './AddTable.svelte';
	import TableRow from './TableRow.svelte';

	export let timelineStart;
	export let room;
</script>

<div class="roomRow">
	<div class="roomHeader">
		<div class="roomName">
			{room.name}
		</div>
		<AddTable roomId={room.id} />
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
</div>

<style>
	.roomHeader {
		display: flex;
		align-items: center;
		background-color: gainsboro;
		width: 100vw;
		padding: 10px 0px;
	}
	.roomName {
		margin-left: 5px;
		margin-right: 20px;
		font-size: larger;
	}
	.roomRow {
	}
</style>
