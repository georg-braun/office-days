<script>
	import { refreshEmployees } from '../backend-service';
	import { employeeStore } from '../store.js';
	import EmployeeRow from './EmployeeRow.svelte';
	import { onMount } from 'svelte';
	import { addDays, sameDay } from '../dateUtils';
	import { columnSize, todayColor} from '../constants';

	

	const timelineStart = getMonday();
	const today = new Date();

	onMount(async () => {
		await refreshEmployees();
	});

	function getMonday() {
		let d = new Date();
		var day = d.getDay(),
			diff = d.getDate() - day + (day == 0 ? -6 : 1); 
		const monday = new Date(d.setDate(diff));
		console.log(monday);
		return monday;
	}

	export function addAndFormatDate(date, days) {
		const currentDate = addDays(date, days);

		return `${currentDate.getDate()}.${currentDate.getMonth() + 1}`;
	}

	function getEmployeeRowColor(value) {

		return value % 2 == 0 ? 'skyblue' : 'white';
	}

	function getHeaderColor(currentDate){
		if (sameDay(today, currentDate)){
			return todayColor;
		};
		return "white";
	}
</script>

<div style="display: flex; ">
	<div class="w-20 h-10">
		<img src="pete.png" alt="panda" />
	</div>
	<div style="min-width: 140px;" />
	{#each Array(14) as _, i}
		<div style="min-width: {columnSize}px; text-align: center; background: {getHeaderColor(addDays(timelineStart, i))};">
			{addAndFormatDate(timelineStart, i)}
		</div>
	{/each}
</div>
{#each $employeeStore as employee, i}
	<EmployeeRow {employee} index="i" timelineStart={timelineStart} backgroundColor={getEmployeeRowColor(i)} />
{/each}

<button class="button block" title="Aktualisieren" on:click={refreshEmployees}> 🔃 </button>
