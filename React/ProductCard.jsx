import React, { useEffect, useState } from "react";
import { ListGroup, Image, Card } from "react-bootstrap";
import { Link } from "react-router-dom";
import PropTypes from "prop-types";
import PriceOdometer from "./PriceOdometer";
import debug from "sabio-debug";
import { loadStripe } from "@stripe/stripe-js";
import Toastr from "toastr";
import * as checkoutService from "../../services/checkoutService";
import * as ProductService from "../../services/productService";

const ProductCard = ({ content, isPricingMode, currentUser, customer }) => {
  const _logger = debug.extend("ProductCard");
  const stripeKey = process.env.REACT_APP_STRIPE_KEY;
  const [productDetail, setProductDetail] = useState({
    amount: 0,
    id: 0,
    name: "",
    priceId: "",
    productId: "",
    term: "",
  });

  let plan = content;

  useEffect(() => {
    const contentId = isPricingMode ? content.id + 1 : content.id;
    ProductService.GetProductById(contentId)
      .then(onGetProductDetailSuccess)
      .catch(onGetProductDetailError);
  }, [isPricingMode]);

  const onGetProductDetailSuccess = (response) => {
    const product = response.data.item;

    setProductDetail((prevState) => {
      const pd = { ...prevState };
      pd.amount = product.amount;
      pd.id = product.id;
      pd.name = product.name;
      pd.priceId = product.priceId;
      pd.productId = product.productId;
      pd.term = product.term;
      return pd;
    });
  };
  const onGetProductDetailError = (error) => {
    _logger(error);
    Toastr.error(
      "Unable to display product details",
      "Please refresh the page"
    );
  };

  const onButtonClick = (e) => {
    e.preventDefault();
    const sessionRequestObj = {
      priceId: e.currentTarget.id,
      currentUserEmail: currentUser.email,
      name: customer.firstName + " " + customer.lastName,
    };

    checkoutService
      .CreateCheckoutSession(sessionRequestObj)
      .then(onCreateCheckoutSessionSuccess)
      .catch(onCreateCheckoutSessionError);
  };

  const onCreateCheckoutSessionSuccess = async (response) => {
    const checkoutSessionId = response.data.item;
    const stripePromise = await loadStripe(stripeKey);
    stripePromise.redirectToCheckout({
      sessionId: checkoutSessionId,
    });
  };
  const onCreateCheckoutSessionError = (error) => {
    _logger(error);
    Toastr.error("Error Creating Checkout Session", "Error");
  };

  return (
    <Card className="border-0 mb-3">
      <Card.Body className="p-0">
        <div className="p-5 text-center">
          <Image src="" alt="" className="mb-5" />
          <div className="mb-5">
            <h2 className="fw-bold">{productDetail.name}</h2>
            <p
              className="mb-0"
              dangerouslySetInnerHTML={{ __html: plan.description }}
            ></p>
          </div>
          <div className="d-flex justify-content-center mb-4">
            <span className="h3 mb-0 fw-bold">$</span>
            <PriceOdometer value={productDetail.amount} />
            <span className="align-self-end mb-1 ms-2 toggle-price-content">
              /{isPricingMode ? "Yearly" : "Monthly"}
            </span>
          </div>
          <div className="d-grid">
            <Link
              to="/#"
              id={productDetail.priceId}
              onClick={onButtonClick}
              className={`btn btn-${
                plan.buttonClass ? plan.buttonClass : "outline-primary"
              }`}
            >
              {plan.buttonText}
            </Link>
          </div>
        </div>
        <hr className="m-0" />
        <div className="p-5">
          <h4 className="fw-bold mb-4">{plan.featureHeading}</h4>
          {/* List of features */}
          <ListGroup bsPrefix="list-unstyled ">
            {plan.features.map((item, index) => {
              return (
                <ListGroup.Item
                  key={index}
                  className="mb-1"
                  bsPrefix="list-item"
                >
                  <span className="text-success me-2">
                    <i className="far fa-check-circle"></i>
                  </span>
                  <span
                    dangerouslySetInnerHTML={{ __html: item.feature }}
                  ></span>
                </ListGroup.Item>
              );
            })}
          </ListGroup>
          <ListGroup bsPrefix="list-unstyled ">
            {plan.unavailableFeatures
              ? plan.unavailableFeatures.map((item, index) => {
                  return (
                    <ListGroup.Item
                      key={index}
                      className="mb-1"
                      bsPrefix="list-item"
                    >
                      <span className="text-muted me-2">
                        <i className="far fa-check-circle"></i>
                      </span>
                      <span
                        className="text-muted me-2"
                        dangerouslySetInnerHTML={{ __html: item.feature }}
                      ></span>
                    </ListGroup.Item>
                  );
                })
              : ""}
          </ListGroup>
        </div>
      </Card.Body>
    </Card>
  );
};

ProductCard.propTypes = {
  isPricingMode: PropTypes.bool.isRequired,
  content: PropTypes.shape({
    id: PropTypes.number,
    image: PropTypes.string,
    plantitle: PropTypes.string,
  }).isRequired,
  currentUser: PropTypes.shape({
    id: PropTypes.number,
    email: PropTypes.string,
    isLoggedIn: PropTypes.bool,
  }).isRequired,
  customer: PropTypes.shape({
    firstName: PropTypes.string,
    lastName: PropTypes.string,
  }).isRequired,
};
export default ProductCard;
