<template>
  <div class="col s12 m7">
    <h2 class="header"
      v-if="orders.length > 0"
      >Orders</h2>
    <h2 class="header"
      v-else
      >Orders Not Found</h2>
    <div class="card horizontal"
      v-for="(order) of orders"
            :key="order._id"
    >
      <div class="card-image small">
        <img style="width: 250px; height: 400px;" v-bind:src="order.product.productImage">
      </div>
      <div class="card-stacked">
        <div class="card-content">
          <p>{{order.product.name}}</p>
          <p>{{'Price: \$' + order.product.price}}</p>
          <p>{{'Quantity: ' + order.quantity}}</p>
        </div>
        <div class="card-action">
      <button class="btn btn-small" v-on:click="btnDelete(order._id)">Delete</button>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  data: () => ({
    products: '',
    orders: '',
    srcImage: '',
    price: ''
  }),
  methods: {
    btnDelete(id) {
      this.$http.delete(
        'http://localhost:3000/orders/' + id,        
        {
          headers: 
          {
            'Authorization': 'Bearer ' + localStorage.getItem('token')
          }
        }
        )
        .then(
          (res) => {
            alert(res.body.message);
          }, (res) =>
          {          
            alert(res.body.message);
            if (res.status == '401')
            {
              this.$router.push('/authorization');          
            }
          }
        )
        
    }
  },
  created() {
    this.$http.get(
      'http://localhost:3000/orders/',        
      {
        headers: 
        {
          'Authorization': 'Bearer ' + localStorage.getItem('token')
        }
      }
      )
      .then(
        (res) => {
          this.orders = res.body.orders;
        }, (res) =>
        {          
          alert(res.body.message);
          if (res.status == '401')
          {
            this.$router.push('/authorization');          
          }
        }
      )
  }  
}
</script>

<style lang="scss" scoped>
</style>