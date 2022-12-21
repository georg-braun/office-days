<script>
	import { employeeNameStore } from '../store';
	import { get } from 'svelte/store';
	import { toggleReservation, deleteTable } from '../backend-service';

	import { columnSize } from '../constants';
	import { sameDay, addDays } from '../dateUtils';
	import { getTodayMarker } from './TodayMarker';

	const availableText = '🪑';
	const reservedText = '🔒';
	export let table;
	export let roomId;

	export let timelineStart;

	function getReservationIcon(table, date) {
		console.log(table)
		if (table.reservations == undefined) return availableText;
		const isReserved = table.reservations.find((_) => sameDay(new Date(_.date), date));
		if (isReserved == undefined) return availableText;
		return reservedText;
	}

	function getReservationEmployee(table, date) {
		console.log(table)
		if (table.reservations == undefined) return "";
		const reservation = table.reservations.find((_) => sameDay(new Date(_.date), date));
		if (reservation == undefined) return "";
		return reservation.who ?? "";
	}

	function updateReservation(date) {
		const dateObj = new Date(date);
		const employeeName = get(employeeNameStore)
		toggleReservation(roomId, table.id, dateObj, employeeName);
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
					{getReservationIcon(table, addDays(timelineStart, i))}</button
				>
				<div class="employee-name" title={getReservationEmployee(table, addDays(timelineStart, i))}>
					{getReservationEmployee(table, addDays(timelineStart, i))}
				</div>	
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

<style>
	.employee-name {	
		padding: 0px 3px;
		margin-bottom: 5px;
		text-align: center;
		font-size: x-small;
		width: 50px;
		text-overflow: ellipsis;
		white-space: nowrap;
		overflow: scroll;
	}
</style>