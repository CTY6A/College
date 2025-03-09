<template>
  <div class="row">
      <h1>{{this.product.name}}</h1>
      <img style="weight: 500px; height: 700px" v-bind:src="product.productImage" >
      <p>{{'Price: \$' + product.price}}</p>
      <a class="btn-floating btn-small waves-effect waves-light red" v-on:click="quantity > 1 ? quantity-- : quantity"><i class="material-icons">remove</i></a> {{quantity}}
      <a class="btn-floating btn-small waves-effect waves-light red" style="margin-right: 1rem;" v-on:click="quantity++"><i class="material-icons">add</i></a>
      <button class="btn btn-small" v-on:click="btnBuy">Buy</button>
  </div>
</template>

<script>
export default {
  data: () => ({
    product: '',
    order: '',
    quantity: ''
  }),
  created() {
    this.quantity = 1;
    this
      .$http
      .get(
        'http://localhost:3000/products/'+this.$route.params.id
        )
      .then(
        (res) => 
        {
          this.product = res.body.product; 
          console.log(res);
        }, 
        (err) => 
        {
          alert(err.message);
          console.log(err);
        }
        )
  }, 
  methods: {
    btnBuy() {
      this
        .$http
        .post(
          'http://localhost:3000/orders/',
          {
            'productId': this.product._id,
            'quantity': this.quantity
          },          
          {
            headers: 
            {
              'Authorization': 'Bearer ' + localStorage.getItem('token')
            }
          })
        .then(
          (res) => {
            this.$router.push('/orders')
          },
          (err) => 
          {
            if (err.status == '401')
            {
              alert(err.message);
              this.$router.push('/authorization');          
            }
          }
          )
    }
  }
}
</script>

<style lang="scss" scoped>

</style>