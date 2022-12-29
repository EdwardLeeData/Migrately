import React, { useEffect, useState } from "react";
import { ListGroup, Image, Card } from "react-bootstrap";
import { Link } from "react-router-dom";
import PropTypes from "prop-types";
import PriceOdometer from "./PriceOdometer";
import debug from "sabio-debug";
import { loadStripe } from "@stripe/stripe-js";
import Swal from "sweetalert2";
import { RightCornerRibbon } from "react-ribbons";
import * as checkoutService from "../../services/checkoutService";
import * as productService from "../../services/productService";

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
  const [currentPlan, setCurrentPlan] = useState({
    id: 0,
    name: "",
  });

  let plan = content;

  useEffect(() => {
    const contentId = isPricingMode ? content.id + 1 : content.id;
    _logger("cId", contentId);
    productService
      .GetProductById(contentId)
      .then(onGetProductDetailSuccess)
      .catch(onGetDetailError);
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
    productService
      .GetCurrentSubscription()
      .then(onGetCurrentSubscriptionSuccess)
      .catch(onGetDetailError);
  };
  const onGetCurrentSubscriptionSuccess = (response) => {
    const subscriptionObj = response.data.item;
    _logger(subscriptionObj);
    setCurrentPlan((prevState) => {
      const pd = { ...prevState };
      pd.id = subscriptionObj.id;
      pd.name = subscriptionObj.name;
      return pd;
    });
  };
  const onGetDetailError = (error) => {
    _logger(error);
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
    Swal.fire("Please Login");
  };

  return (
    <Card className="border-0 mb-3">
      {currentPlan.name === productDetail.name ? (
        <RightCornerRibbon
          backgroundColor="#cc0000"
          color="#f0f0f0"
          fontFamily="Arial"
        >
          CURRENT
        </RightCornerRibbon>
      ) : (
        ""
      )}

      <Card.Body className="p-0">
        <div className="p-5 text-center">
          <Image src="" alt="" className="mb-5" />
          <div className="mb-5">
            <h2 className="fw-bold">{productDetail.name}</h2>
          </div>
          <div className="d-flex justify-content-center mb-4">
            <span className="h3 mb-0 fw-bold">$</span>
            <PriceOdometer value={productDetail?.amount} />
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
              {currentPlan.name === productDetail.name
                ? "Your Plan"
                : currentPlan.name
                ? "Change to this Plan"
                : plan.buttonText}
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
