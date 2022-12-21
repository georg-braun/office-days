<script>
	import { refreshEmployees } from '../backend-service';
	import { employeeStore } from '../store.js';
	import EmployeeRow from './EmployeeRow.svelte';
	import { onMount } from 'svelte';
	import { addDays, sameDay } from '../dateUtils';
	import { columnSize, todayColor } from '../constants';
	import { getTodayMarker } from './TodayMarker';

	const today = new Date();

	onMount(async () => {
		await refreshEmployees();
	});

	export let timelineStart;
	export function addAndFormatDate(date, days) {
		const currentDate = addDays(date, days);

		return `${currentDate.getDate()}.${currentDate.getMonth() + 1}`;
	}

	function getEmployeeRowColor(value) {
		return value % 2 == 0 ? 'skyblue' : 'white';
	}
</script>

<div style="display: flex; ">
	<div style="min-width: 140px;" />
	{#each Array(14) as _, i}
		<div
			class="mt-auto"
			style="min-width: {columnSize}px; text-align: center; vertical-align: bottom;"
		>
			{#if sameDay(today, addDays(timelineStart, i))}
				<div>
					<img src="pete.png" alt="panda" width="30" />
				</div>
			{/if}
		</div>
	{/each}
</div>
<div style="display: flex; ">
	<div style="min-width: 140px;" />

	{#each Array(14) as _, i}
		<div
			class="mt-auto"
			style="min-width: {columnSize}px; text-align: center; vertical-align: bottom; {getTodayMarker(
				addDays(timelineStart, i)
			)};"
		>
			{addAndFormatDate(timelineStart, i)}
		</div>
	{/each}
</div>
<!-- {#each $employeeStore as employee, i}
	<EmployeeRow {employee} index="i" {timelineStart} backgroundColor={getEmployeeRowColor(i)} />
{/each}
 -->
<!-- <button class="button block" title="Aktualisieren" on:click={refreshEmployees}> 🔃 </button>
 -->