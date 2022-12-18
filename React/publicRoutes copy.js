import { lazy } from "react";

const CheckoutSuccess = lazy(() =>
  import("../components/checkout/CheckoutSuccess")
);
const Products = lazy(() => import("../components/checkout/Products"));
const ProcessingProducts = lazy(() =>
  import("../components/checkout/CheckoutProcessing")
);

const routes = [
  {
    path: "/products/success",
    name: "CheckoutSuccess",
    exact: true,
    element: CheckoutSuccess,
    roles: [],
    isAnonymous: true,
  },
  {
    path: "/products",
    name: "Products",
    exact: true,
    element: Products,
    roles: [],
    isAnonymous: true,
  },
  {
    path: "/products/processing",
    name: "ProcessingProducts",
    exact: true,
    element: ProcessingProducts,
    roles: [],
    isAnonymous: true,
  },
];

var allRoutes = [...routes];

export default allRoutes;
