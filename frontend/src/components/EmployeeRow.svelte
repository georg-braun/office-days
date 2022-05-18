<script>
	import { visitOffice, stayHome, deleteEmployee } from '../backend-service';

	import { columnSize, todayColor } from '../constants';
	import { sameDay, addDays } from '../dateUtils';

	export let index;
	export let employee;
	export let timelineStart;
	export let backgroundColor;
	let homeText = '🏠';
	let today = new Date();

	function getBackgroundColor(currentDate) {
		if (sameDay(today, currentDate)) {
			return todayColor;
		}
		return backgroundColor;
	}

	function getLocation(employee, date) {
		var dayOfWeek = date.getDay();
		var isWeekend = dayOfWeek === 6 || dayOfWeek === 0; // 6 = Saturday, 0 = Sunday
		if (isWeekend) return '😎';

		// this could be done better. Gets called for every employee within the timerange and the office days are checked each time.
		if (employee.officeDays == undefined) return homeText;
		const isOfficeDay = employee.officeDays.find((_) => sameDay(new Date(_), date));
		if (isOfficeDay == undefined) return homeText;
		return '🏭';
	}

	function updateLocation(id, date, oldLocation) {
		var dayOfWeek = date.getDay();
		var isWeekend = dayOfWeek === 6 || dayOfWeek === 0; // 6 = Saturday, 0 = Sunday
		if (isWeekend) return;

		if (date == undefined) {
			console.log('date is undefined');
			return;
		}

		const dateObj = new Date(date);

		if (oldLocation == homeText) {
			// -> go to the office => add date
			console.log('go the office');
			visitOffice(id, dateObj);
		} else {
			// -> work from home => remove date
			console.log('work from home');
			stayHome(id, dateObj);
		}
	}
</script>

<div>
	<span />

	<div style="display: flex">
		<div
			style="background-color: {backgroundColor}; min-width: 140px; max-width: 140px; display: flex; align-items: center;"
		>
			{employee.name}
		</div>

		{#each Array(14) as _, i}
			<div
				style="background-color: {getBackgroundColor(
					addDays(timelineStart, i)
				)}; min-width: {columnSize}px; text-align: center;"
			>
				<button
					style="padding: 2; margin: 0; border: none; background: transparent; font-size: x-large;"
					on:click={() =>
						updateLocation(
							employee.id,
							addDays(timelineStart, i),
							getLocation(employee, addDays(timelineStart, i))
						)}
				>
					<!-- this is a litte bit weird. The employee has to be an argument to detect the change and run when the employee data changes-->
					{getLocation(employee, addDays(timelineStart, i))}</button
				>
			</div>
		{/each}
		<button
			style="border: none; background: transparent;"
			on:click={() => {
				if (confirm('Willst du die Person wirklich löschen?')) deleteEmployee(employee.id);
			}}>🗑️</button
		>
	</div>
</div>
