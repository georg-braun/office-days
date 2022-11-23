<script>
	import { refreshRooms } from '../backend-service';
	import { roomStore } from '../store.js';
	import EmployeeRow from './EmployeeRow.svelte';
	import { onMount } from 'svelte';
	import { addDays, sameDay } from '../dateUtils';
	import { columnSize, todayColor } from '../constants';
	import { getTodayMarker } from './TodayMarker';
	import RoomRow from './RoomRow.svelte';

	export let timelineStart;
	const today = new Date();

	onMount(async () => {
		await refreshRooms();
	});


	export function addAndFormatDate(date, days) {
		const currentDate = addDays(date, days);

		return `${currentDate.getDate()}.${currentDate.getMonth() + 1}`;
	}

	function getEmployeeRowColor(value) {
		return value % 2 == 0 ? 'skyblue' : 'white';
	}

</script>


{#each $roomStore as room, i}
	<RoomRow room={room} {timelineStart} />
{/each}

