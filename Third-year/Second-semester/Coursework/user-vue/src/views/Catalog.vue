<template>

<div class="col s12 m7">
    <h2 class="header">Catalog</h2>
    <div class="card horizontal"
      v-for="(product) of products"
            :key="product._id"
    >
      <div class="card-image small">
        <img style="width: 250px; height: 400px;" v-bind:src="product.productImage">
      </div>
      <div class="card-stacked">
        <div class="card-content">
          <p>{{product.name}}</p>
          <p>{{'Price: \$' + product.price}}</p>
        </div>
        <div class="card-action">
          <router-link :to="'/product/' + product._id">More details</router-link>
        </div>
      </div>
    </div>
  </div>


</template>

<script>
export default {
  data: () => ({
    products: ''
  }),
  created() {
    this.$http.get('http://localhost:3000/products').then((res) => {
      this.products = res.body.products;
      console.log(res);
    })
  }
}
</script>
