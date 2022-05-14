<script>
  import { refreshEmployees } from "./backend-service";
  import { employeeStore } from "./store.js";
  //import EmployeeRow from "./EmployeeRow.svelte";
  //import { onMount } from "svelte";
  //import { addDays } from "./dateUtils";
  //import { columnSize } from "./constants";

/*
  const today = new Date();

  onMount(async () => {
    //employeeStore.set([{id: 0, name: "Georg", office_days:[new Date("2022-05-13"), new Date("2022-05-15")]}, {id: 1, name: "Karsten", office_days:[]}])
    await refreshEmployees();
  });

  export function addAndFormatDate(date, days) {
    const currentDate = addDays(date, days);

    return `${currentDate.getDate()}.${currentDate.getMonth() + 1}`;
  }

  function getColor(value) {
    return value % 2 == 0 ? "skyblue" : "white";
  }
  */
</script>

<!--
<div style="display: flex; ">
  <div style="min-width: {columnSize}px;" />
  {#each Array(14) as _, i}
    <div style="min-width: {columnSize}px; text-align: center;">
      {addAndFormatDate(today, i)}
    </div>
  {/each}
</div>
{#each $employeeStore as employee, i}

  <EmployeeRow
    {employee}
    index="i"
    today={new Date()}
    backgroundColor={getColor(i)}
  />

{/each}

<button class="button block" on:click={refreshEmployees}> 🔃 </button>

-->