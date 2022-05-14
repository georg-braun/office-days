<script>
  import { supabase } from './supabaseClient'
  import {addEmployee} from "./backend-service"

  let name = "";


  async function getProfile() {
    try {
      loading = true
      const user = supabase.auth.user()

      let { data, error, status } = await supabase
        .from('profiles')
        .select(`username, website, avatar_url`)
        .eq('id', user.id)
        .single()

      if (error && status !== 406) throw error

      if (data) {
        username = data.username
        website = data.website
        avatar_url = data.avatar_url
      }
    } catch (error) {
      alert(error.message)
    } finally {
      loading = false
    }
  }



  async function addNewEmployee() {
    try {

      addEmployee(name);
       /*
      const updates = {
        name: name,       
        //office_days: [new Date("2022-05-13"), new Date("2022-05-15")] 
      }

     
      let { error } = await supabase.from('employee').upsert(updates, {
        returning: 'minimal', // Don't return the value after inserting
      })
      */

      //if (error) throw error
    } catch (error) {
      alert(error.message)
    } finally {
      
    }
  }
</script>


<input type="text" placeholder="Name" bind:value={name} />
<button class="button block" on:click={addNewEmployee}>
  Hinzufügen
</button>