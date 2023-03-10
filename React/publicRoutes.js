import { lazy } from "react";
const Landing = lazy(() => import("../components/landing/Landing"));
const PageNotFound = lazy(() => import("../components/error/Error404"));

const CheckoutSuccess = lazy(() =>
  import("../components/checkout/CheckoutSuccess")
);
const Products = lazy(() => import("../components/checkout/Products"));
const ProcessingProducts = lazy(() =>
  import("../components/checkout/CheckoutProcessing")
);

const routes = [
  {
    path: "/",
    name: "Landing",
    exact: true,
    element: Landing,
    roles: [],
    isAnonymous: true,
  },
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

const errorRoutes = [
  {
    path: "*",
    name: "Error - 404",
    element: PageNotFound,
    roles: [],
    exact: true,
    isAnonymous: true,
  },
];

var allRoutes = [
  ...routes,
  ...errorRoutes,
];

export default allRoutes;
