<template>
    <div>
        <nav class="blue darken-3">
            <div class="nav-wrapper">
            <ul>
                <router-link
                    tag="li"
                    to="/"
                    exact
                    active-class="active"
                >
                    <a href="#">Catalog</a>
                </router-link>
                <router-link
                    tag="li"
                    to="/orders"
                    active-class="active"
                >
                    <a href="#">Orders</a>
                </router-link>
                <router-link
                    tag="li"
                    to="/authorization"
                    active-class="active"
                >
                    <a href="#">Authorization</a>
                </router-link>
            </ul>
            </div>
        </nav>
    </div>
</template>

<script>
export default {
    
}
</script>

<style lang="scss" scoped>
</style>