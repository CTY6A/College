<template>
<div class="row">
    <form class="col s6 offset-s3" @submit.prevent="submitHandler">
      <div class="row">
        <div class="input-field col s12">
          <input v-model="email" id="email_inline" type="email" class="validate">
            <label for="email_inline">Email</label>
            <span class="helper-text" data-error="wrong" data-success="right">Helper text</span>
        </div>
      </div>
      <div class="row">
        <div class="input-field col s12">
          <input v-model="password" id="password" type="password" class="validate">
          <label for="password">Password</label>
        </div>
      </div>
      <button class="btn" type="submit" style="margin-right: 1rem;">Log In</button>
      <button class="btn blue" type="button" @click="signUp" style="margin-right: 1rem;">Sign Up</button>      
      <button class="btn red" type="button" @click="signOut">Sign Out</button>
    </form>
  </div>
</template>

<script>
export default {
  methods: {
    submitHandler() {
      this
        .$http
        .post
        (
          'http://localhost:3000/user/login',
          {          
            email: this.email,
            password: this.password          
          }
        )
        .then(
          (res) => {
            alert(res.body.message)
            localStorage.setItem('token', res.body.token)
          },
          (res) => 
          {
            alert(res.body.message)
          })
    },
    signUp() {
      this
        .$http
        .post
        (
          'http://localhost:3000/user/signup',
          {          
            email: this.email,
            password: this.password          
          }
        )
        .then(
          (res) => {
            alert(res.body.message)
          },
          (res) => 
          {
            alert(res.body.message)
          })
    },
    signOut() {
      localStorage.removeItem('token');
      alert('Sign Out successful')
    }
  }
}
</script>

<style lang="scss" scoped>

</style>