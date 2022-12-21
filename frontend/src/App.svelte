<script>
	import AddEmployee from './components/AddEmployee.svelte';
	import AddRoom from './components/AddRoom.svelte';
	import Overview from './components/Overview.svelte';
	import RoomOverview from './components/RoomOverview.svelte';


	const timelineStart = getMonday();

	function getMonday() {
		let d = new Date();
		var day = d.getDay(),
			diff = d.getDate() - day + (day == 0 ? -6 : 1);
		const monday = new Date(d.setDate(diff));
		console.log(monday);
		return monday;
	}
</script>

<div class="container" style="padding: 20px 0 20px 0;">
	<h1>👨🏾‍💼 👩🏻‍💼 🐶 🐱 🦍 🐼 🦄 Office days?</h1>

	<Overview {timelineStart} />
	<div class="add-employee">
		<AddEmployee />
	</div>

	<RoomOverview {timelineStart} />
	<div class="add-room">
		<AddRoom />
	</div>



	<div style="margin-top: 30px;">
		<b>A Slack Time Production 🚀</b>
	</div>
</div>

<style>
		.add-employee{
		margin-top: 10px;
	}
	.add-room{
		margin-top: 10px;
	}
</style>
