<script>
	import { bookTable as toggleReservation, deleteTable } from '../backend-service';

	import { columnSize } from '../constants';
	import { sameDay, addDays } from '../dateUtils';
	import { getTodayMarker } from './TodayMarker';

	const availableText = '🪑';
	const reservedText = '🔒';
	export let table;
	export let roomId;

	export let timelineStart;

	function getReservation(table, date) {
		if (table.reservedDates == undefined) return availableText;
		const isReserved = table.reservedDates.find((_) => sameDay(new Date(_), date));
		if (isReserved == undefined) return availableText;
		return reservedText;
	}

	function updateReservation(date) {
		const dateObj = new Date(date);
		toggleReservation(roomId, table.id, dateObj, '');
	}
</script>

<div>
	<span />

	<div style="display: flex">
		<div style=" min-width: 140px; max-width: 140px; display: flex; align-items: center;">
			{table.name}
		</div>

		{#each Array(14) as _, i}
			<div
			style="{getTodayMarker(
				addDays(timelineStart, i)
			)}; min-width: {columnSize}px; text-align: center;"
			>
				<button
					style="padding: 2; margin: 0; border: none; background: transparent; font-size: x-large;"
					on:click={() => updateReservation(addDays(timelineStart, i))}
				>
					<!-- this is a litte bit weird. The employee has to be an argument to detect the change and run when the employee data changes-->
					{getReservation(table, addDays(timelineStart, i))}</button
				>
			</div>
		{/each}

		<button
			style="margin-left: 5px; border: none; background: transparent;"
			on:click={() => {
				if (confirm('Willst du den Tisch wirklich löschen?')) deleteTable(roomId, table.id);
			}}>🗑️</button
		>
	</div>
</div>
