<script>

import {visitOffice, stayHome, deleteEmployee} from "./backend-service"

  import { columnSize } from "./constants";

  export let index;
  export let employee;
  export let today;
  export let backgroundColor;
  let homeText = "🏠";

  function addDays(date, days) {
    let newDate = new Date(date);
    newDate.setDate(newDate.getDate() + days);
    return newDate;
  }

  function getLocation(employee, date) {      

    var dayOfWeek = date.getDay();
    var isWeekend = (dayOfWeek === 6) || (dayOfWeek  === 0); // 6 = Saturday, 0 = Sunday
    if (isWeekend)
      return "😎"

    // this could be done better. Gets called for every employee within the timerange and the office days are checked each time.
    if (employee.officeDays == undefined) return homeText;
    const isOfficeDay = employee.officeDays.find((_) =>
      sameDay(new Date(_), date)
    );
    if (isOfficeDay == undefined) return homeText;
    return "🏭";
  }

  function updateLocation(id, date, oldLocation) {
    
    var dayOfWeek = date.getDay();
    var isWeekend = (dayOfWeek === 6) || (dayOfWeek  === 0); // 6 = Saturday, 0 = Sunday
    if (isWeekend)
      return;

    if (date == undefined) {
      console.log("date is undefined");
      return;
    }


    const dateObj=new Date(date);

  
    if (oldLocation == homeText) {
      // -> go to the office => add date
      console.log("go the office");      
      visitOffice(id, dateObj);
    } else {
      // -> work from home => remove date
      console.log("work from home");      
      stayHome(id, dateObj)      
    }  
  }


  function sameDay(d1, d2) {
    return (
      d1.getFullYear() === d2.getFullYear() &&
      d1.getMonth() === d2.getMonth() &&
      d1.getDate() === d2.getDate()
    );
  }
</script>

<div>
  <span />

  <div style="display: flex">
    <div
      style="background-color: {backgroundColor} ; min-width: {columnSize}px; "
    >
      {employee.name}
    </div>

    {#each Array(14) as _, i}
      <div
        style="background-color: {backgroundColor}; min-width: {columnSize}px; text-align: center;"
      >
        <button
          style="padding: 2; margin: 0; border: none; background: transparent;"
          on:click={() =>
            updateLocation(
              employee.id,
              addDays(today, i),
              getLocation(employee, addDays(today, i))
            )}>
            <!-- this is a litte bit weird. The employee has to be an argument to detect the change and run when the employee data changes-->
            {getLocation(employee, addDays(today, i))}</button
        >
      </div>
    {/each}
    <button style="border: none; background: transparent;" on:click="{deleteEmployee(employee.id)}">🗑️</button>
  </div>
</div>
