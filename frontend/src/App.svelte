<script>
  
  import AddEmployee from "./AddEmployee.svelte"
  import Overview from "./Overview.svelte"

  

  /*
  employeeStore.set([{Name: "Georg"}])

  user.set(supabase.auth.user())

  supabase.auth.onAuthStateChange((_, session) => {
      user.set(session.user)
  })
  */
</script>

<div class="container" style="padding: 50px 0 100px 0;">

  
  <Overview />
  <AddEmployee />

  <div style="margin-top: 30px;">
  <b>A Slack Time Production 🚀</b>
</div>

  

  <!--
  {#if $user}
      <Profile />
  {:else}
      <Auth />
  {/if}
  -->
</div>